package com.github.gimmi.spikejsr330.application;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import com.github.gimmi.spikejsr330.framework.FrameworkModule;
import com.google.inject.Guice;

public class AppServletContextListener implements ServletContextListener {
	@Override
	public void contextInitialized(ServletContextEvent evt) {
		Guice.createInjector(new ApplicationModule(), new FrameworkModule());
	}

	@Override
	public void contextDestroyed(ServletContextEvent sce) {
	}
}
