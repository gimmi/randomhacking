package spikeDirectJNgine;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

import javax.servlet.http.HttpSessionEvent;
import javax.servlet.http.HttpSessionListener;

public class AppSessionListener implements HttpSessionListener {

	@Override
	public void sessionCreated(HttpSessionEvent se) {
		Connection connection;
		try {
			connection = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1\\SQLEXPRESS;database=Northwind;","sa", "");
		} catch (SQLException e) {
			throw new RuntimeException(e);
		}
		se.getSession().setAttribute("conn", connection);
	}

	@Override
	public void sessionDestroyed(HttpSessionEvent se) {
		Connection conn = (Connection) se.getSession().getAttribute("conn");
		try {
			conn.close();
		} catch (SQLException e) {
			throw new RuntimeException(e);
		}
	}
}
