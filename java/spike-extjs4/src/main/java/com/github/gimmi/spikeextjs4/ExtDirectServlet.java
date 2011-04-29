package com.github.gimmi.spikeextjs4;

import com.google.gson.*;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.io.Writer;
import java.lang.reflect.Method;
import java.util.HashMap;
import java.util.Map;

public abstract class ExtDirectServlet extends HttpServlet {
	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		PrintWriter writer = buildResponseWriter(resp, "text/javascript", "UTF-8");
		writer.write(String.format("%s = ", getApiName()));
		getGson().toJson(buildApi(req.getRequestURI()), writer);
		writer.write(";");
	}

	private JsonObject buildApi(String url) {
		JsonObject api = new JsonObject();
		api.addProperty("url", url);
		api.addProperty("type", "remoting");
		api.addProperty("namespace", getNamespace());
		JsonObject actions = new JsonObject();
		api.add("actions", actions);
		JsonArray methods = new JsonArray();
		for (Method method : getMethods().values()) {
			JsonObject obj = new JsonObject();
			obj.addProperty("name", method.getName());
			obj.addProperty("len", method.getParameterTypes().length);
			methods.add(obj);
		}
		actions.add(getActionName(), methods);
		return api;
	}

	private String getNamespace() {
		return "Ns";
	}

	private Gson getGson() {
		return new GsonBuilder().setPrettyPrinting().create();
	}

	private String getActionName() {
		return getClass().getSimpleName();
	}

	private String getApiName() {
		return "REMOTING_API";
	}

	private Map<String, Method> getMethods() {
		Map<String, Method> ret = new HashMap<String, Method>();
		for (Method method : getClass().getMethods()) {
			if (method.getAnnotation(ExtDirect.class) != null) {
				ret.put(method.getName(), method);
			}
		}
		return ret;
	}

	@Override
	protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		JsonElement jsonReq = new JsonParser().parse(req.getReader());
		JsonElement jsonResp = processRequestArray(jsonReq);
		getGson().toJson(jsonResp, buildResponseWriter(resp, "application/json", "UTF-8"));
	}

	private JsonElement processRequestArray(JsonElement json) {
		if (json.isJsonArray()) {
			JsonArray ret = new JsonArray();
			for (JsonElement item : json.getAsJsonArray()) {
				ret.add(processRequest(item.getAsJsonObject()));
			}
			return ret;
		} else {
			return processRequest(json.getAsJsonObject());
		}
	}

	private JsonElement processRequest(JsonObject req) {
		Request request = new Request();
		request.action = req.get("action").getAsString();
		request.method = req.get("method").getAsString();
		request.type = req.get("type").getAsString();
		request.tid = req.get("tid").getAsInt();
		request.data = req.get("data").getAsJsonArray();

		invoke(request);

		JsonObject resp = new JsonObject();
		resp.addProperty("action", request.action);
		resp.addProperty("method", request.method);
		resp.addProperty("type", request.type);
		resp.addProperty("tid", request.tid);
		resp.addProperty("message", request.message);
		resp.addProperty("where", request.where);
		resp.add("result", request.result);
		return resp;
	}

	private void invoke(Request request) {
		Method method = getMethods().get(request.method);
		Class<?>[] parameterTypes = method.getParameterTypes();
		Object[] paramValues = new Object[parameterTypes.length];
		try {
			for (int i = 0; i < parameterTypes.length; i++) {
				paramValues[i] = getGson().fromJson(request.data.get(i), parameterTypes[i]);
			}
			request.result = getGson().toJsonTree(method.invoke(this, paramValues), method.getReturnType());
		} catch (Throwable e) {
			request.type = "exception";
			request.message = e.getMessage();
			request.where = getStackTrace(e);
		}
	}

	public static String getStackTrace(Throwable throwable) {
		Writer result = new StringWriter();
		throwable.printStackTrace(new PrintWriter(result));
		return result.toString();
	}

	private static class Request {
		public String action;
		public String method;
		public String type;
		public Integer tid;
		public JsonArray data;
		public JsonElement result;
		public String message;
		public String where;
	}

	public static PrintWriter buildResponseWriter(HttpServletResponse response, String contentType, String charset) throws IOException {
		response.setStatus(HttpServletResponse.SC_OK);
		// See http://stackoverflow.com/questions/3613161/tomcat-server-file-download-problem-with-encoding
		response.setCharacterEncoding(charset);
		response.setContentType(contentType + ";charset=" + charset);
		return response.getWriter();
	}
}
