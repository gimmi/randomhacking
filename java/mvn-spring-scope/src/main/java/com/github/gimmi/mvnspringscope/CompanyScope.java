package com.github.gimmi.mvnspringscope;

import org.springframework.beans.factory.ObjectFactory;
import org.springframework.beans.factory.config.Scope;

public class CompanyScope implements Scope {
	private static final ThreadLocal<CompanyScopeData> attributesHolder = new ThreadLocal<>();

	public static void begin() {
		if (attributesHolder.get() != null) {
			throw new IllegalStateException("CompanyScope already active for thread");
		}
		attributesHolder.set(new CompanyScopeData());
	}

	public static void end() {
		CompanyScopeData data = attributesHolder.get();
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

	private CompanyScopeData getData() {
		CompanyScopeData data = attributesHolder.get();
		if (data == null) {
			throw new IllegalStateException("CompanyScope not active");
		}
		return data;
	}
}
