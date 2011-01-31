package com.github.gimmi.spikejsr330.application;

import com.github.gimmi.spikejsr330.framework.FrameworkModule;
import com.google.inject.Guice;
import com.google.inject.Injector;
import com.google.inject.servlet.GuiceServletContextListener;

public class AppServletContextListener extends GuiceServletContextListener {

	@Override
	protected Injector getInjector() {
		return Guice.createInjector(new ApplicationModule(), new FrameworkModule());
	}
}
