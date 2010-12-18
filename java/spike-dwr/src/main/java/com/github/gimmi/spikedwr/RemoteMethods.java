package com.github.gimmi.spikedwr;

import java.util.Date;
import java.lang.Double;

public class RemoteMethods {
	public String simpleParametersEcho(Integer intValue, String stringValue, Boolean boolValue, Date dateValue, Double doubleValue) {
		return String.format("%s, %s, %s, %s, %s", intValue, stringValue, boolValue, dateValue, doubleValue);
	}
}
