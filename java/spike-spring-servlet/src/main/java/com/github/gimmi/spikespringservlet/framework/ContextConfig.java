package com.github.gimmi.spikespringservlet.framework;

import java.sql.Connection;
import java.util.Properties;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Scope;
import org.springframework.context.annotation.ScopedProxyMode;
import org.springframework.web.servlet.handler.SimpleUrlHandlerMapping;

import com.github.gimmi.spikespringservlet.application.ConnectionProvider;

@Configuration
public class ContextConfig {
	@Autowired
	ConnectionProvider connectionProvider;

	@Bean
	public Repository repository() {
		return new Repository(conn());
	}

	@Bean
	public TestServlet testServlet() {
		return new TestServlet(repository());
	}

	@Bean
	@Scope(value = "request", proxyMode = ScopedProxyMode.INTERFACES)
	public Connection conn() {
		return connectionProvider.build();
	}

	@Bean
	public SimpleUrlHandlerMapping simpleUrlHandlerMapping() {
		SimpleUrlHandlerMapping ret = new SimpleUrlHandlerMapping();
		Properties mappings = new Properties();
		mappings.put("/testServlet", testServlet());
		ret.setMappings(mappings);
		return ret;
	}
}
