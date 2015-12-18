package com.github.gimmi.mvnspringscope;

import org.springframework.beans.factory.ObjectFactory;
import org.springframework.beans.factory.config.Scope;

import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Supplier;

public class ThreadLocalUowScope implements Scope {
	private static final ThreadLocal<ScopeData> attributesHolder = new ThreadLocal<>();

	public static void begin() {
		if (attributesHolder.get() != null) {
			throw new IllegalStateException(String.format("%s already active for thread %s", ThreadLocalUowScope.class.getSimpleName(), Thread.currentThread().getName()));
		}
		attributesHolder.set(new ScopeData());
	}

	public static void end() {
		ScopeData data = attributesHolder.get();
		if (data != null) {
			data.executeCallbacks();
			attributesHolder.remove();
		}
	}

	@Override
	public Object get(String name, ObjectFactory<?> objectFactory) {
		return getData().getAttr(name, objectFactory::getObject);
	}

	@Override
	public Object remove(String name) {
		return getData().remove(name);
	}

	@Override
	public void registerDestructionCallback(String name, Runnable callback) {
		getData().registerDestructionCallback(name, callback);
	}

	@Override
	public Object resolveContextualObject(String key) {
		return null;
	}

	@Override
	public String getConversationId() {
		return null;
	}

	private ScopeData getData() {
		ScopeData data = attributesHolder.get();
		if (data == null) {
			throw new IllegalStateException(String.format("%s not active for thread %s", ThreadLocalUowScope.class.getSimpleName(), Thread.currentThread().getName()));
		}
		return data;
	}

	private static class ScopeData {
		private final Map<String, Object> attributes = new HashMap<>();
		private final Map<String, Runnable> callbacks = new HashMap<>();

		public Object getAttr(String name, ObjectFactory<?> objectFactory) {
			Object attr = attributes.get(name);
			if (attr == null) {
				attr = objectFactory.getObject();
				attributes.put(name, attr);
			}
			return attr;
		}

		public Object remove(String name) {
			Object attr = attributes.get(name);
			attributes.remove(name);
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
}
