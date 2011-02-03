package com.github.gimmi.spikejsr330.framework;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.google.inject.Inject;
import com.google.inject.servlet.RequestScoped;

@RequestScoped
public class Repository {
	private final Logger logger = LoggerFactory.getLogger(Repository.class);
	private final Database database;

	@Inject
	public Repository(Database database) {
		logger.info("ctor");
		this.database = database;
	}

	public void doWork() {
		database.doWork();
	}

}
