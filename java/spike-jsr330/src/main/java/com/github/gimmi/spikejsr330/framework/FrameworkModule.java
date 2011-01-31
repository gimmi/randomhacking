package com.github.gimmi.spikejsr330.framework;

import com.google.inject.AbstractModule;
import com.google.inject.Singleton;

public class FrameworkModule extends AbstractModule {
	@Override
	protected void configure() {
		bind(Database.class);
		bind(Repository.class);
		bind(TestServlet.class).in(Singleton.class);
		install(new FrameworkServletModule());
	}
}
