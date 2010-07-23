package spikeAppEngine;

import java.io.IOException;
import java.util.logging.Logger;

import javax.servlet.http.*;

@SuppressWarnings("serial")
public class SpikeAppEngineServlet extends HttpServlet {
	private static final Logger log = Logger.getLogger(SpikeAppEngineServlet.class.getName());

	private static org.apache.log4j.Logger log4j = org.apache.log4j.Logger.getLogger(SpikeAppEngineServlet.class);

	public void doGet(HttpServletRequest req, HttpServletResponse resp) throws IOException {
		resp.setContentType("text/plain");
		resp.getWriter().println("Hello, world");
		log.severe("Severe logging message just to test logging configuration");
		log4j.error("test log4j message");
	}
}
