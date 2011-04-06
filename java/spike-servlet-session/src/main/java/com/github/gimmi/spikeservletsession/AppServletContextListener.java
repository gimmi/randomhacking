package com.github.gimmi.spikeservletsession;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import java.util.logging.Logger;

public class AppServletContextListener implements ServletContextListener {
	static Logger logger = Logger.getLogger(AppServletContextListener.class.getName());

	@Override
	public void contextInitialized(ServletContextEvent evt) {

		try {
			Class.forName("org.apache.derby.jdbc.EmbeddedDriver").newInstance();
		} catch (Exception e) {
			throw new RuntimeException(e);
		}

		logger.info("App version " + getAppVersion() + " context initialized");
	}

	@Override
	public void contextDestroyed(ServletContextEvent evt) {
		logger.info("App version " + getAppVersion() + " context destroyed");
	}

	private String getAppVersion() {
		return getClass().getPackage().getImplementationVersion();
	}
}
