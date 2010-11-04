package com.github.gimmi.spikespringservlet;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.HttpRequestHandler;

public class TestServlet implements HttpRequestHandler {
	private static final Logger logger = LoggerFactory.getLogger(TestServlet.class);

	@Override
	public void handleRequest(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		logger.warn("X");
	}
}
