package com.github.gimmi.spikemavengae.data;

import java.util.List;
import java.util.Map;
import java.util.Set;

import com.google.appengine.api.datastore.DatastoreService;
import com.google.appengine.api.datastore.Entity;
import com.google.appengine.api.datastore.Key;
import com.google.appengine.api.datastore.Query;
import com.google.appengine.repackaged.com.google.common.collect.Lists;
import com.google.appengine.repackaged.com.google.common.collect.Maps;
import com.google.appengine.repackaged.com.google.common.collect.Sets;

public class ObjectDatastore {
	private final Map<Class<?>, EntityConverter<?>> ec = Maps.newHashMap();
	private final DatastoreService ds;

	public ObjectDatastore(DatastoreService ds) {
		this.ds = ds;
	}

	public <T> void registerConverter(Class<T> type, EntityConverter<? super T> converter) {
		ec.put(type, converter);
	}

	public void put(Object obj) {
		Set<Entity> puts = Sets.newHashSet();
		List<Key> deletes = Lists.newArrayList();
		Entity root = add(puts, obj, null);
		Query query = new Query().setAncestor(root.getKey()).setKeysOnly();
		for (Entity entity : ds.prepare(query).asIterable()) {
			if (!puts.contains(entity)) {
				deletes.add(entity.getKey());
			}
		}
		ds.delete(deletes);
		ds.put(puts);
	}

	@SuppressWarnings({ "rawtypes", "unchecked" })
	private Entity add(Set<Entity> entities, Object obj, Key parent) {
		EntityConverter converter = ec.get(obj.getClass());
		return add(entities, obj, converter, parent);
	}

	private <T> Entity add(Set<Entity> entities, T obj, EntityConverter<? super T> converter, Key parent) {
		Entity entity = converter.toEntity(obj, parent);
		entities.add(entity);
		for (Object child : converter.getChildren()) {
			add(entities, child, entity.getKey());
		}
		return entity;
	}
}
