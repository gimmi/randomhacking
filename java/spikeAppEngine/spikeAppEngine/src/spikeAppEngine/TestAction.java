package spikeAppEngine;

import java.util.logging.Logger;

import com.softwarementors.extjs.djn.config.annotations.DirectMethod;

public class TestAction {
	private static final Logger log = Logger.getLogger(TestAction.class.getName());

	@DirectMethod
	public String doEcho(String data) {
		log.severe("Method called with " + data);
		return data;
	}

	@DirectMethod
	public double multiply(String num) {
		return Double.parseDouble(num) * 8;
	}
}
