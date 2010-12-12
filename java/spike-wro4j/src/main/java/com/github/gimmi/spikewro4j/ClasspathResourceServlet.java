package com.github.gimmi.spikewro4j;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ClasspathResourceServlet extends HttpServlet {

	private static final long serialVersionUID = -730144935993409307L;
	private static final Logger logger = LoggerFactory.getLogger(ClasspathResourceServlet.class);

	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws IOException {
		String pathInfo = req.getPathInfo();
		InputStream inputStream = Thread.currentThread().getContextClassLoader().getResourceAsStream(pathInfo);
		if (inputStream == null) {
			resp.sendError(HttpServletResponse.SC_NOT_FOUND, "Not found");
			logger.trace("Classpath resource not found: {}", pathInfo);
		} else {
			String mimeType = getServletContext().getMimeType(pathInfo);
			if (mimeType == null) {
				mimeType = "application/octet-stream";
			}
			resp.setStatus(HttpServletResponse.SC_OK);
			resp.setContentType(mimeType);
			transferStreams(inputStream, resp.getOutputStream());
			logger.info("Classpath resource served: '{}' mime type: '{}'", pathInfo, mimeType);
		}
	}

	protected static void transferStreams(InputStream is, OutputStream os) throws IOException {
		try {
			byte[] buf = new byte[4 * 1024];
			int bytesRead;
			while ((bytesRead = is.read(buf)) != -1) {
				os.write(buf, 0, bytesRead);
			}
		} finally {
			is.close();
			os.close();
		}
	}
}
