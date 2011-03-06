package com.github.gimmi.spikemavengae.data;

import com.google.appengine.api.datastore.Entity;
import com.google.appengine.api.datastore.Key;

public interface EntityConverter<T> {
	String getKind();

	T fromEntity(Entity entity);

	Entity toEntity(T obj, Key parent);

	Iterable<Object> getChildren(T obj);

	void setChild(T obj, Object child);
}
