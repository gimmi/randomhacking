package com.github.gimmi.trackr.configuration;

import java.io.File;
import java.io.FileInputStream;
import java.util.logging.LogManager;
import java.util.logging.Logger;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

public class LoggingConfigurationServletContextListener implements ServletContextListener {
	@Override
	public void contextInitialized(ServletContextEvent sce) {
		File config = new File(sce.getServletContext().getRealPath("/"), "WEB-INF/logging.properties");
		try {
			LogManager.getLogManager().readConfiguration(new FileInputStream(config));
		} catch (Exception e) {
			throw new RuntimeException(e);
		}
		Logger logger = Logger.getLogger(LoggingConfigurationServletContextListener.class.getName());
		logger.info("Logging configured from file " + config);
	}

	@Override
	public void contextDestroyed(ServletContextEvent sce) {
	}
}
