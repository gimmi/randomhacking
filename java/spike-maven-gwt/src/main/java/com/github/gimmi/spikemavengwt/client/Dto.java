package com.github.gimmi.spikemavengwt.client;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class Dto implements Serializable {
	String address;
	String name;

	public Dto(String name, String address) {
		this.name = name;
		this.address = address;
	}

	public Dto() {
	}
}