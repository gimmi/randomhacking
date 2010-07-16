package spikeDirectJNgine;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import org.apache.log4j.xml.DOMConfigurator;

public class AppContextListener implements ServletContextListener {

	@Override
	public void contextInitialized(ServletContextEvent evt) {
		String prefix = evt.getServletContext().getRealPath("/");
		DOMConfigurator.configure(prefix + "WEB-INF/log4j.xml");

		new com.microsoft.sqlserver.jdbc.SQLServerDriver();
	}

	@Override
	public void contextDestroyed(ServletContextEvent arg0) {
		
	}

}
