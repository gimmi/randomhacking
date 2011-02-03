package com.github.gimmi.spikejsr330.framework;

import java.sql.Connection;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.google.inject.Inject;
import com.google.inject.Provider;
import com.google.inject.servlet.RequestScoped;

@RequestScoped
public class Database {
	private final Logger logger = LoggerFactory.getLogger(Database.class);
	private final Provider<Connection> connection;

	@Inject
	public Database(Provider<Connection> connection) {
		logger.info("ctor");
		this.connection = connection;
	}

	public void doWork() {
		try {
			connection.get().close();
		} catch (SQLException e) {
			throw new RuntimeException(e);
		}
	}
}
