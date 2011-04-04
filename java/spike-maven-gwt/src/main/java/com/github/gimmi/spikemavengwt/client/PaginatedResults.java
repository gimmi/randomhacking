package com.github.gimmi.spikemavengwt.client;

import java.io.Serializable;
import java.util.List;

public class PaginatedResults<T> implements Serializable {
	List<T> results;
	int totalRows;

	public PaginatedResults() {
	}

	public PaginatedResults(List<T> results, int totalRows) {
		this.results = results;
		this.totalRows = totalRows;
	}
}
