package com.github.gimmi.spikejsr330.application;

import java.util.logging.Logger;

import org.slf4j.bridge.SLF4JBridgeHandler;

import com.github.gimmi.spikejsr330.framework.FrameworkModule;
import com.google.inject.Guice;
import com.google.inject.Injector;
import com.google.inject.servlet.GuiceServletContextListener;

public class AppGuiceServletContextListener extends GuiceServletContextListener {
	@Override
	protected Injector getInjector() {
		Logger.getLogger("com.google.inject").addHandler(new SLF4JBridgeHandler());
		return Guice.createInjector(new ApplicationModule(), new FrameworkModule());
	}
}
