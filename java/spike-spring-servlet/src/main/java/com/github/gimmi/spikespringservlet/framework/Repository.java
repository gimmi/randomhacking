package com.github.gimmi.spikespringservlet.framework;

import java.sql.Connection;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Repository {
	private static final Logger logger = LoggerFactory.getLogger(Repository.class);
	private final Connection connection;

	public Repository(Connection connection) {
		logger.warn("ctor");
		this.connection = connection;
	}

	public void query() {
		logger.warn("query");
		try {
			connection.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}
}
