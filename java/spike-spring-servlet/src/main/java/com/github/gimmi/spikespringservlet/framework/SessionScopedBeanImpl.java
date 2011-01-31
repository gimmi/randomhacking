package com.github.gimmi.spikespringservlet.framework;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class SessionScopedBeanImpl implements SessionScopedBean {
	private static final Logger logger = LoggerFactory.getLogger(SessionScopedBeanImpl.class);

	public SessionScopedBeanImpl() {
		logger.info("ctor");
	}

	@Override
	public void doSomethig() {
		logger.info("doSomethig");
	}
}
