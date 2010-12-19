package com.github.gimmi.spikedwr;

public class LookupValue {
	private Object value;
	private String descr;

	public LookupValue() {
		this(null, null);
	}

	public LookupValue(Object value, String descr) {
		this.value = value;
		this.descr = descr;
	}

	public Object getValue() {
		return this.value;
	}

	public void setValue(Object value) {
		this.value = value;
	}

	public String getDescr() {
		return this.descr;
	}

	public void setDescr(String descr) {
		this.descr = descr;
	}
}
