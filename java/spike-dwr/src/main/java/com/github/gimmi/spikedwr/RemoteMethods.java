package com.github.gimmi.spikedwr;

import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class RemoteMethods {
	private static final Logger logger = LoggerFactory.getLogger(RemoteMethods.class);

	public String simpleParametersEcho(Integer intValue, String stringValue, Boolean boolValue, Date dateValue, Double doubleValue) {
		return String.format("%s, %s, %s, %s, %s", intValue, stringValue, boolValue, dateValue, doubleValue);
	}

	public Date dateEcho(Date date) {
		logger.info("date={}", date);
		return date;
	}
}
