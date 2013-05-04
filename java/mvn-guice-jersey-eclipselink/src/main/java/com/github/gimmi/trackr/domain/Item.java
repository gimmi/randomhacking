package com.github.gimmi.trackr.domain;

import javax.persistence.*;
import java.util.*;

@Entity
@Table(name = "ITEMS")
public class Item extends BaseEntity {
	@Version
	private int version;

	private String title;

	@ManyToMany
	@JoinTable(name="ITEMS_TAGS", joinColumns={@JoinColumn(name="ITEM_ID", referencedColumnName="ID")}, inverseJoinColumns={@JoinColumn(name="TAG_ID", referencedColumnName="ID")})
	private Set<Tag> tags = new HashSet<>();

	public int getVersion() {
		return version;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public Set<Tag> getTags() {
		return tags;
	}
}
