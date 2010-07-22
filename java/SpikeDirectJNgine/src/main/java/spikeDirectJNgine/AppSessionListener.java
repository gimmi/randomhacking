package spikeDirectJNgine;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

import javax.servlet.http.HttpSessionEvent;
import javax.servlet.http.HttpSessionListener;

import org.apache.log4j.Logger;

public class AppSessionListener implements HttpSessionListener {
	static Logger logger = Logger.getLogger(AppSessionListener.class);
	
	@Override
	public void sessionCreated(HttpSessionEvent se) {
		String connectionString = se.getSession().getServletContext().getInitParameter("connectionString");

		Connection connection;
		try {
			connection = DriverManager.getConnection(connectionString, "sa", "");
		} catch (SQLException e) {
			throw new RuntimeException(e);
		}
		se.getSession().setAttribute("conn", connection);
		logger.warn("AppSessionListener.sessionCreated");
	}

	@Override
	public void sessionDestroyed(HttpSessionEvent se) {
		Connection conn = (Connection) se.getSession().getAttribute("conn");
		try {
			conn.close();
		} catch (SQLException e) {
			throw new RuntimeException(e);
		}
		logger.warn("AppSessionListener.sessionDestroyed");
	}
}
