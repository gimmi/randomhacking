package com.github.gimmi.spikemavengae.data;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import com.google.appengine.api.datastore.DatastoreService;
import com.google.appengine.api.datastore.Entity;
import com.google.appengine.api.datastore.EntityNotFoundException;
import com.google.appengine.api.datastore.Key;
import com.google.appengine.api.datastore.KeyFactory;
import com.google.appengine.api.datastore.Query;
import com.google.appengine.repackaged.com.google.common.base.Objects;
import com.google.appengine.repackaged.com.google.common.collect.Lists;
import com.google.appengine.repackaged.com.google.common.collect.Maps;
import com.google.appengine.repackaged.com.google.common.collect.Sets;

public class ObjectDatastore {
	private final Map<Class<?>, EntityConverter<?>> typeMap = Maps.newHashMap();
	private final Map<String, EntityConverter<?>> kindMap = Maps.newHashMap();
	private final DatastoreService ds;

	public ObjectDatastore(DatastoreService ds) {
		this.ds = ds;
	}

	public <T> void registerConverter(Class<T> type, EntityConverter<? super T> converter) {
		typeMap.put(type, converter);
		kindMap.put(converter.getKind(), converter);
	}

	public void put(Object obj) {
		Set<Entity> entities = Sets.newHashSet();
		Entity root = addEntities(entities, obj, null);
		ds.delete(getDeletedEntities(root.getKey(), entities));
		ds.put(entities);
	}

	@SuppressWarnings({ "unchecked", "rawtypes" })
	public <T> T get(Class<T> type, String id) {
		EntityConverter converter = typeMap.get(type);
		Key rootKey = KeyFactory.createKey(converter.getKind(), id);
		Entity rootEntity;
		try {
			rootEntity = ds.get(rootKey);
		} catch (EntityNotFoundException e) {
			throw new RuntimeException(e);
		}
		Map<Key, List<Entity>> children = getChildren(rootKey);
		return (T) toObj(rootEntity, children);
	}

	@SuppressWarnings({ "rawtypes", "unchecked" })
	private Object toObj(Entity entity, Map<Key, List<Entity>> children) {
		EntityConverter converter = kindMap.get(entity.getKind());
		Object obj = converter.fromEntity(entity);
		for (Entity childEntity : Objects.firstNonNull(children.get(entity.getKey()), new ArrayList<Entity>())) {
			converter.setChild(obj, toObj(childEntity, children));
		}
		return obj;
	}

	private Iterable<Key> getDeletedEntities(Key root, Set<Entity> puts) {
		List<Key> deletes = Lists.newArrayList();
		Query query = new Query().setAncestor(root).setKeysOnly();
		for (Entity entity : ds.prepare(query).asIterable()) {
			if (!puts.contains(entity)) {
				deletes.add(entity.getKey());
			}
		}
		return deletes;
	}

	private Map<Key, List<Entity>> getChildren(Key rootKey) {
		Map<Key, List<Entity>> directChildren = Maps.newHashMap();
		Query query = new Query().setAncestor(rootKey);
		for (Entity entity : ds.prepare(query).asIterable()) {
			Key parentKey = entity.getKey().getParent();
			if (!directChildren.containsKey(parentKey)) {
				directChildren.put(parentKey, new ArrayList<Entity>());
			}
			directChildren.get(parentKey).add(entity);
		}
		return directChildren;
	}

	@SuppressWarnings({ "unchecked", "rawtypes" })
	private Entity addEntities(Set<Entity> entities, Object obj, Key parent) {
		EntityConverter converter = typeMap.get(obj.getClass());
		Entity entity = converter.toEntity(obj, parent);
		entities.add(entity);
		for (Object child : Objects.firstNonNull(converter.getChildren(obj), new ArrayList<Object>(0))) {
			addEntities(entities, child, entity.getKey());
		}
		return entity;
	}
}
