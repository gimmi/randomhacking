package com.github.gimmi.spikejsr330.framework;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.google.inject.Inject;
import com.google.inject.Provider;
import com.google.inject.Singleton;

@Singleton
public class TestServlet extends HttpServlet {
	private static final long serialVersionUID = 2201562246783765952L;
	public Logger logger = LoggerFactory.getLogger(TestServlet.class);
	private final Provider<Repository> repository;

	@Inject
	public TestServlet(Provider<Repository> repository) {
		logger.info("ctor");
		this.repository = repository;
	}

	@Override
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		logger.info("doGet");
		repository.get().doWork();
	}
}
