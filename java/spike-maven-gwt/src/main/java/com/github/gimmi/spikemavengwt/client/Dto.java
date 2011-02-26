package com.github.gimmi.spikemavengwt.client;

import java.util.Arrays;
import java.util.List;

public class Dto {
	public static final List<Dto> DATA = Arrays.asList(new Dto("John", "123 Fourth Road"), new Dto("Mary", "222 Lancer Lane"));

	final String address;
	final String name;

	public Dto(String name, String address) {
		this.name = name;
		this.address = address;
	}
}