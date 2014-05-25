package com.github.gimmi.trackr.configuration;

import com.github.gimmi.trackr.web.HelloWorldServlet;
import com.github.gimmi.trackr.web.ItemResource;
import com.google.inject.Guice;
import com.google.inject.Injector;
import com.google.inject.servlet.GuiceServletContextListener;
import com.google.inject.servlet.ServletModule;
import com.sun.jersey.guice.spi.container.servlet.GuiceContainer;
import org.codehaus.jackson.JsonParser;
import org.codehaus.jackson.jaxrs.JacksonJsonProvider;

import java.util.HashMap;
import java.util.Map;

public class AppServletContextListener extends GuiceServletContextListener {
    @Override
    protected Injector getInjector() {
        return Guice.createInjector(new ServletModule() {
            @Override
            protected void configureServlets() {
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
        });
    }
}
