package spikeDirectJNgine;

import javax.servlet.http.HttpServlet;

import org.apache.log4j.Logger;
import org.apache.log4j.xml.DOMConfigurator;

public class WebAppSetup extends HttpServlet {
	private static final long serialVersionUID = 1L;
	protected static Logger logger = Logger.getLogger(WebAppSetup.class);

	public void init() {
		String prefix = getServletContext().getRealPath("/");
		String file = getInitParameter("log4j-init-file");
		DOMConfigurator.configure(prefix + file);
	}
}
