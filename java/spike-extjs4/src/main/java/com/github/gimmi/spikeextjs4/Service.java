package com.github.gimmi.spikeextjs4;

public class Service extends ExtDirectServlet {
	@ExtDirect
	public String echo(String value) {
		return value.toUpperCase();
	}
}
