package com.github.gimmi.spikemavengwt.client;

import com.google.gwt.user.cellview.client.Column;

public class ColumnInfo<T> {
	final Column<T, ?> column;
	final String header;

	public ColumnInfo(String header, Column<T, ?> column) {
		this.column = column;
		this.header = header;
	}
}
