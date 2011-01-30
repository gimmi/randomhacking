package com.github.gimmi.spikejsr330;

import com.google.inject.AbstractModule;
import com.google.inject.Guice;
import com.google.inject.Injector;
import com.google.inject.Singleton;
import com.google.inject.servlet.GuiceServletContextListener;
import com.google.inject.servlet.ServletModule;

public class AppServletContextListener extends GuiceServletContextListener {

	@Override
	protected Injector getInjector() {
		return Guice.createInjector(new AppModule(), new AppServletModule());
	}

	public static class AppModule extends AbstractModule {
		@Override
		protected void configure() {
//			bindScope(RequestScoped.class, ServletScopes.REQUEST);
			bind(Database.class);
			bind(Repository.class);
			bind(TestServlet.class).in(Singleton.class);
		}
	}

	public static class AppServletModule extends ServletModule {
		@Override
		protected void configureServlets() {
			serve("/TestServlet").with(TestServlet.class);
		}
	}
}
