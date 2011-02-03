package com.github.gimmi.spikejsr330.application;

import java.sql.Connection;

import com.google.inject.AbstractModule;
import com.google.inject.servlet.SessionScoped;

public class ApplicationModule extends AbstractModule {
	@Override
	protected void configure() {
		bind(Connection.class).toProvider(ConnectionProvider.class).in(SessionScoped.class);
		install(new ApplicationServletModule());
	}
}