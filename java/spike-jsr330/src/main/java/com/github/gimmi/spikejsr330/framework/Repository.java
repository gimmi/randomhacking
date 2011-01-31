package com.github.gimmi.spikejsr330.framework;

import com.google.inject.Inject;

public class Repository {

	private final Database database;

	@Inject
	public Repository(Database database) {
		this.database = database;
	}

}
