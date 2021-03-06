package com.github.gimmi.spikespringservlet.framework;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.HttpRequestHandler;

public class TestServlet implements HttpRequestHandler {
	private static final Logger logger = LoggerFactory.getLogger(TestServlet.class);
	private final Repository repository;

	public TestServlet(Repository repository) {
		logger.warn("ctor");
		this.repository = repository;
	}

	@Override
	public void handleRequest(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		logger.warn("handleRequest");
		repository.query();
	}
}
