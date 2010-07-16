package spikeDirectJNgine;

import com.softwarementors.extjs.djn.config.annotations.DirectMethod;

public class TestAction {
	@DirectMethod
	public String doEcho(String data) {
		return data;
	}

	@DirectMethod
	public double multiply(String num) {
		return Double.parseDouble(num) * 8;
	}
}
