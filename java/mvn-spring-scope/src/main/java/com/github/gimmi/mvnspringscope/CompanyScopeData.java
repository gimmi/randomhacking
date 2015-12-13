package com.github.gimmi.mvnspringscope;

import java.util.HashMap;
import java.util.Map;
import java.util.function.Supplier;

public class CompanyScopeData {
	private final Map<String, Object> attributes = new HashMap<>();
	private final Map<String, Runnable> callbacks = new HashMap<>();

	public Object getAttr(String name, Supplier factory) {
		Object attr = attributes.get(name);
		if (attr == null) {
			attr = factory.get();
			attributes.put(name, attr);
		}
		return attr;
	}

	public Object remove(String name) {
		Object attr = attributes.get(name);
		if (attr != null) {
			attributes.remove(name);
		}
		callbacks.remove(name);
		return attr;
	}

	public void registerDestructionCallback(String name, Runnable callback) {
		callbacks.put(name, callback);
	}

	public void executeCallbacks() {
		callbacks.values().forEach(Runnable::run);
	}
}
