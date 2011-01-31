package com.github.gimmi.spikespringservlet.application;

import java.sql.Connection;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ConnectionProvider {
	private static final Logger logger = LoggerFactory.getLogger(ConnectionProvider.class);

	public Connection build() {
		logger.info("Building Connection");
		return new ConnectionImpl();
	}
}
