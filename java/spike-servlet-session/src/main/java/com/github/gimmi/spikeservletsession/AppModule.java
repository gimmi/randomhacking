package com.github.gimmi.spikeservletsession;

import com.google.inject.AbstractModule;
import com.google.inject.Provides;
import com.google.inject.servlet.SessionScoped;
import sun.jdbc.odbc.JdbcOdbcConnection;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.UUID;
import java.util.logging.Logger;

public class AppModule extends AbstractModule {
	static Logger logger = Logger.getLogger(AppModule.class.getName());
	
	@Override
	protected void configure() {
		install(new AppServletModule());
	}

	@Provides
	@SessionScoped
	Connection provideConnection() {
		Connection conn;

		logger.info("Creating JDBC connection");
		
		try {
			conn = DriverManager.getConnection("jdbc:derby:" + UUID.randomUUID().toString() + ";create=true");
		} catch (SQLException e) {
			throw new RuntimeException("Getting database connection", e);
		}

		return conn;
	}

}
