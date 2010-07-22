package spikeDirectJNgine;

import java.sql.Connection;

import javax.servlet.http.HttpSession;

import org.apache.log4j.Logger;

import com.softwarementors.extjs.djn.config.annotations.DirectMethod;
import com.softwarementors.extjs.djn.servlet.ssm.WebContextManager;

public class TestAction {
	static Logger logger = Logger.getLogger(AppContextListener.class);

	@DirectMethod
	public String doEcho(String data) {
		HttpSession session = WebContextManager.get().getSession();

		@SuppressWarnings("unused")
		Connection conn = (Connection) session.getAttribute("conn");

		logger.warn("TestAction.doEcho");
		return data;
	}

	@DirectMethod
	public double multiply(String num) {
		return Double.parseDouble(num) * 8;
	}
}
