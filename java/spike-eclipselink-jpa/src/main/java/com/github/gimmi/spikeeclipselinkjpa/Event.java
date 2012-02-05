package com.github.gimmi.spikeeclipselinkjpa;

import javax.persistence.*;
import java.util.Date;
import java.util.UUID;

@Entity
public class Event {
	@Id
	private UUID id = UUID.randomUUID();

	private String title;

	@Temporal(TemporalType.DATE)
	private Date date;

	@Version
	private int version;

	public int getVersion() {
		return version;
	}

	public UUID getId() {
		return id;
	}

	public Date getDate() {
		return date;
	}

	public void setDate(Date date) {
		this.date = date;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}
}