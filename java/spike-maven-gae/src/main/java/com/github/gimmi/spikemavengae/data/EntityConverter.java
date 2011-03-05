package com.github.gimmi.spikemavengae.data;

import java.util.Collection;

import com.google.appengine.api.datastore.Entity;
import com.google.appengine.api.datastore.Key;

public interface EntityConverter<T> {
	T fromEntity(Entity entity);

	Entity toEntity(T obj, Key parent);

	Collection<Object> getChildren();
}
