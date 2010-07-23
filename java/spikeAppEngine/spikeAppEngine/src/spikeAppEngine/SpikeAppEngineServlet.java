package spikeAppEngine;

import java.io.IOException;
import java.util.logging.Logger;

import javax.servlet.http.*;

@SuppressWarnings("serial")
public class SpikeAppEngineServlet extends HttpServlet {
	private static final Logger log = Logger.getLogger(SpikeAppEngineServlet.class.getName());

	public void doGet(HttpServletRequest req, HttpServletResponse resp) throws IOException {
		resp.setContentType("text/plain");
		resp.getWriter().println("Hello, world");
		log.severe("Severe logging message just to test logging configuration");
	}
}
