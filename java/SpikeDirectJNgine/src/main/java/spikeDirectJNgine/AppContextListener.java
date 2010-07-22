package spikeDirectJNgine;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import org.apache.log4j.Logger;
import org.apache.log4j.xml.DOMConfigurator;

public class AppContextListener implements ServletContextListener {
	static Logger logger = Logger.getLogger(AppContextListener.class);

	@Override
	public void contextInitialized(ServletContextEvent evt) {
		String prefix = evt.getServletContext().getRealPath("/");
		DOMConfigurator.configure(prefix + "WEB-INF/log4j.xml");

		new com.microsoft.sqlserver.jdbc.SQLServerDriver();

		logger.warn("AppContextListener.contextInitialized");
	}

	@Override
	public void contextDestroyed(ServletContextEvent arg0) {
		logger.warn("AppContextListener.contextDestroyed");
	}

}
