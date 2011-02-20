package com.github.gimmi.spikejul;

import java.io.IOException;
import java.io.InputStream;
import java.util.logging.LogManager;
import java.util.logging.Logger;

import org.junit.Test;

public class JulTest {
	@Test
	public void tt() throws SecurityException, IOException {
		InputStream inputStream = getClass().getResourceAsStream("logging.properties");
		LogManager.getLogManager().readConfiguration(inputStream);

		Logger logger = Logger.getLogger("com.google.inject");
		logger.finest("this is finest");
//		logger.finer("this is finer");
//		logger.fine("this is fine");
//		logger.config("this is config");
//		logger.info("this is info");
//		logger.warning("this is a warning");
		logger.severe("this is severe");
	}

}
