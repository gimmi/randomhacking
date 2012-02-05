package com.github.gimmi.spikeeclipselinkjpa;

import javax.persistence.*;
import java.util.*;

@Entity
public class Task {
	@Id
	private UUID id = UUID.randomUUID();

	private String title;

	@Temporal(TemporalType.DATE)
	private Date date;

	@Version
	private int version;

	@OneToMany(mappedBy = "task", cascade = CascadeType.ALL)
	private Collection<Comment> comments = new HashSet<Comment>();

	public Collection<Comment> getComments() {
		return Collections.unmodifiableCollection(comments);
	}

	public void addComment(Comment comment) {
		if (comment.task != null) {
			comment.task.removeComment(comment);
		}
		comment.task = this;
		comments.add(comment);
	}

	public void removeComment(Comment comment) {
		if (comments.contains(comment)) {
			comments.remove(comment);
			comment.task = null;
		}
	}

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