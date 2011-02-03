package com.github.gimmi.spikejsr330.application;

import java.sql.Connection;

import javax.servlet.ServletContext;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.google.inject.Inject;
import com.google.inject.Provider;
import com.google.inject.Singleton;

@Singleton
public class ConnectionProvider implements Provider<Connection> {
	private final Logger logger = LoggerFactory.getLogger(ConnectionProvider.class);
	private final ServletContext servletContext;

	@Inject
	public ConnectionProvider(ServletContext servletContext) {
		this.servletContext = servletContext;
	}

	@Override
	public Connection get() {
		String connectionString = servletContext.getInitParameter("connectionString");
		logger.info("provide connectgionString: {}", connectionString);
		return new ConnectionImpl();
	}

}
