package com.github.gimmi.spikeextjs4;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Service extends ExtDirectServlet {
	private final Logger logger = LoggerFactory.getLogger(Service.class);

	@ExtDirect
	public String echo(String value) {
		logger.info("echo({})", value);
		return value;
	}

	@ExtDirect
	public void primitiveParameters(String stringValue, Integer intValue, Boolean boolValue, Double doubleValue) {
		logger.info("primitiveParameters({}, {}, {}, {})", new Object[]{stringValue, intValue, boolValue, doubleValue});
	}
}
