package com.github.gimmi.spikespringservlet.application;

import java.sql.Connection;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ConnectionBuilder {
	private static final Logger logger = LoggerFactory.getLogger(ConnectionBuilder.class);

	public Connection build() {
		logger.info("Building Connection");
		return new ConnectionImpl();
	}
}
