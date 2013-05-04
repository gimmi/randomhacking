package com.github.gimmi.trackr.configuration;

import com.github.gimmi.trackr.web.HelloWorldServlet;
import com.github.gimmi.trackr.web.ItemResource;
import com.google.inject.Guice;
import com.google.inject.Injector;
import com.google.inject.Singleton;
import com.google.inject.persist.PersistFilter;
import com.google.inject.persist.jpa.JpaPersistModule;
import com.google.inject.servlet.GuiceServletContextListener;
import com.google.inject.servlet.ServletModule;
import com.sun.jersey.guice.spi.container.servlet.GuiceContainer;
import org.codehaus.jackson.JsonParser;
import org.codehaus.jackson.jaxrs.JacksonJsonProvider;
import org.codehaus.jackson.map.DeserializationConfig;
import org.codehaus.jackson.map.SerializationConfig;

import java.util.HashMap;
import java.util.Map;
import java.util.Properties;

public class AppServletContextListener extends GuiceServletContextListener {
	private final Map<String, String> jpaProperties;

	public AppServletContextListener(Map<String, String> jpaProperties) {
		this.jpaProperties = jpaProperties;
	}

	public AppServletContextListener() {
		this(new HashMap<String, String>(0));
	}

	@Override
	protected Injector getInjector() {
		return Guice.createInjector(new ServletModule() {
			@Override
			protected void configureServlets() {
				configureJpaPersistModule();

				serve("/helloworld").with(HelloWorldServlet.class);

				bind(ItemResource.class);

				configureJackson();
			}

			private void configureJackson() {
				JacksonJsonProvider jacksonJsonProvider = new JacksonJsonProvider()
						.configure(JsonParser.Feature.ALLOW_SINGLE_QUOTES, true)
						.configure(JsonParser.Feature.ALLOW_UNQUOTED_FIELD_NAMES, true);
				bind(JacksonJsonProvider.class).toInstance(jacksonJsonProvider);

				Map<String, String> initParams = new HashMap<>();
				initParams.put("com.sun.jersey.config.feature.Trace", "true");
				serve("/api/*").with(GuiceContainer.class, initParams);
			}

			private void configureJpaPersistModule() {
				Properties properties = new Properties();
				properties.putAll(jpaProperties);
				install(new JpaPersistModule("com.github.gimmi.trackr").properties(properties));
				filter("/*").through(PersistFilter.class);
			}
		});
	}
}
