package com.github.gimmi.spikeservletsession;


import com.google.inject.Inject;
import com.google.inject.Provider;
import com.google.inject.Singleton;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.sql.Connection;
import java.util.logging.Logger;

@Singleton
public class TestServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
	static Logger logger = Logger.getLogger(TestServlet.class.getName());
	private Provider<Connection> conn;

	@Inject
	public TestServlet(Provider<Connection> conn) {
		this.conn = conn;
	}

	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws IOException {
		logger.info("GET from session=" + req.getSession().getId());
		Connection connection = conn.get();
		logger.info("Connection is " + (connection != null));
	}
}
