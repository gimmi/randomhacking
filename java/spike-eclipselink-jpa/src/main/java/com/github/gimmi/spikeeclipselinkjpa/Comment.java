package com.github.gimmi.spikeeclipselinkjpa;

import javax.persistence.*;
import java.util.UUID;

@Entity
public class Comment {
	@Id
	@Column(columnDefinition = "CHAR(36)")
	private String id = UUID.randomUUID().toString();

	@ManyToOne
	protected Task task;

	@Version
	private int version;

	public String getId() {
		return id;
	}
}
