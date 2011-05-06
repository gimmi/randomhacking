using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace SpikeHttpHandler
{
	public class ExampleHandler : IHttpHandler
	{
		private static readonly IDictionary<string, HandlerInfo> handlers = new Dictionary<string, HandlerInfo>();
		public static DirectActionFactory HandlerFactory = new DirectActionFactory();
		private readonly JsonSerializer _serializer = new JsonSerializer();

		public bool IsReusable
		{
			get { return false; }
		}

		public static void RegisterType(Type type)
		{
			var handlerInfo = new HandlerInfo{
				Type = type,
				Name = type.Name
			};
			handlers.Add(handlerInfo.Name, handlerInfo);
		}

		public void ProcessRequest(HttpContext context)
		{
			switch(context.Request.HttpMethod)
			{
				case "GET":
					doGet(context.Request, context.Response);
					break;
				case "POST":
					doPost(context.Request, context.Response);
					break;
			}
		}

		private void doPost(HttpRequest request, HttpResponse response)
		{
			SerializeResponse(response, new DirectHandler(HandlerFactory).handle(DeserializeRequest(request)));
		}

		private DirectRequest DeserializeRequest(HttpRequest request)
		{
			var sr = new StreamReader(request.InputStream, request.ContentEncoding);
			using(JsonReader jsonReader = new JsonTextReader(sr))
			{
				return _serializer.Deserialize<DirectRequest>(jsonReader);
			}
		}

		private void SerializeResponse(HttpResponse response, DirectResponse value)
		{
			using (var jsonWriter = new JsonTextWriter(new StreamWriter(response.OutputStream, response.ContentEncoding)))
			{
				_serializer.Serialize(jsonWriter, value);
			}
		}

		private void doGet(HttpRequest request, HttpResponse response)
		{
			response.Write("Hello from custom handler.");
		}

		#region Nested type: HandlerInfo

		private class HandlerInfo
		{
			public Type Type;
			public string Name;
			public IDictionary<string, MethodInfo> Methods = new Dictionary<string, MethodInfo>();
		}

		#endregion
	}
}