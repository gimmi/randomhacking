package com.github.gimmi.spikemavengwt.client;

import java.util.ArrayList;
import java.util.List;

public class Dto {
	public static final List<Dto> DATA = new ArrayList<Dto>();
	static {
		for (int i = 0; i < 100; i++) {
			DATA.add(new Dto("Name " + i, "Address " + i));
		}
	}

	final String address;
	final String name;

	public Dto(String name, String address) {
		this.name = name;
		this.address = address;
	}
}