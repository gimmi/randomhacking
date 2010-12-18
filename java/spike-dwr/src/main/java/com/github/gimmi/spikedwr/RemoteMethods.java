package com.github.gimmi.spikedwr;

import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

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

	public Map<Integer, String> mapWithIntegerKeys(Map<Integer, String> map) {
		map.put(123, "from java");
		return map;
	}

	public List<Map<Integer, Date>> listOfMap(List<Map<Integer, Date>> list) {
		Map<Integer, Date> map = new HashMap<Integer, Date>();
		map.put(123, newDate(2010, 12, 23));
		list.add(map);
		return list;
	}

	private static Date newDate(int year, int month, int day) {
		Calendar calendar = Calendar.getInstance();
		calendar.clear();
		calendar.set(year, month - 1, day, 0, 0, 0);
		return calendar.getTime();
	}

}
