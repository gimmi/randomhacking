package com.github.gimmi.spikedwr;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;

public class DbRow implements Map<String, Object> {
	private HashMap<String, Object> map = new HashMap<String, Object>();

	public DbRow(int id) {
		put("id", id);
	}

	public int getId() {
		return (Integer) get("id");
	}

	@Override
	public int size() {
		return map.size();
	}

	@Override
	public boolean isEmpty() {
		return map.isEmpty();
	}

	@Override
	public Object get(Object key) {
		return map.get(key.toString().toLowerCase());
	}

	@Override
	public boolean containsKey(Object key) {
		return map.containsKey(key.toString().toLowerCase());
	}

	@Override
	public Object put(String key, Object value) {
		return map.put(key.toLowerCase(), value);
	}

	@Override
	public void putAll(Map<? extends String, ? extends Object> m) {
		for (Map.Entry<? extends String, ?> entry : m.entrySet()) {
			this.put(entry.getKey(), entry.getValue());
		}
	}

	@Override
	public Object remove(Object key) {
		return map.remove(key.toString().toLowerCase());
	}

	@Override
	public void clear() {
		map.clear();
	}

	@Override
	public boolean containsValue(Object value) {
		return map.containsValue(value);
	}

	@Override
	public Set<String> keySet() {
		return map.keySet();
	}

	@Override
	public Collection<Object> values() {
		return map.values();
	}

	@Override
	public Set<java.util.Map.Entry<String, Object>> entrySet() {
		return map.entrySet();
	}
}
