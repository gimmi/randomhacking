package com.github.gimmi.spikemavengae.domain;

import java.util.UUID;

public class Comment {
	public String id = UUID.randomUUID().toString();
	public String user;
	public String text;
}
