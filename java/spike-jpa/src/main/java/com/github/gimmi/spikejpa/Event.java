package com.github.gimmi.spikejpa;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Version;
import java.util.Date;
import java.util.UUID;

@Entity
public class Event {
	@Id
	private String id = UUID.randomUUID().toString();
	private String title;
	private Date date;

	@Version
	private Integer version;

	public Integer getVersion() {
		return version;
	}

	public String getId() {
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