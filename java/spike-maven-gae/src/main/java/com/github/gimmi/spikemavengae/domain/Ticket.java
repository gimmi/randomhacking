package com.github.gimmi.spikemavengae.domain;

import java.util.List;
import java.util.UUID;

import com.google.appengine.repackaged.com.google.common.collect.Lists;

public class Ticket {
	public String id = UUID.randomUUID().toString();
	public String title;
	public List<Comment> comments = Lists.newArrayList();
}
