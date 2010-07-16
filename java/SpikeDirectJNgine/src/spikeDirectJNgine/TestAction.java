package spikeDirectJNgine;

import java.sql.Connection;

import javax.servlet.http.HttpSession;

import com.softwarementors.extjs.djn.config.annotations.DirectMethod;
import com.softwarementors.extjs.djn.servlet.ssm.WebContextManager;

public class TestAction {
	@DirectMethod
	public String doEcho(String data) {
		HttpSession session = WebContextManager.get().getSession();
		Connection conn = (Connection) session.getAttribute("conn");
		
		return data;
	}

	@DirectMethod
	public double multiply(String num) {
		return Double.parseDouble(num) * 8;
	}
}
