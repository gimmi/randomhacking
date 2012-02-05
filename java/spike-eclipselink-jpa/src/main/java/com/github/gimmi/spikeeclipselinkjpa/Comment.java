package com.github.gimmi.spikeeclipselinkjpa;

import javax.persistence.*;
import java.util.UUID;

@Entity
public class Comment {
	@Id
	private UUID id = UUID.randomUUID();

	@ManyToOne
	protected Task task;

	@Version
	private int version;

	public UUID getId() {
		return id;
	}
}
