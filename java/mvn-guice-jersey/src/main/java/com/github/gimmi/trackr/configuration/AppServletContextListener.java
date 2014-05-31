package com.github.gimmi.trackr.configuration;

import com.github.gimmi.trackr.web.HelloWorldServlet;
import com.github.gimmi.trackr.web.ItemResource;
import com.google.inject.Guice;
import com.google.inject.Injector;
import com.google.inject.Key;
import com.google.inject.name.Names;
import com.google.inject.servlet.GuiceServletContextListener;
import com.google.inject.servlet.ServletModule;
import com.sun.jersey.guice.spi.container.servlet.GuiceContainer;
import net.jawr.web.servlet.JawrServlet;
import org.codehaus.jackson.JsonParser;
import org.codehaus.jackson.jaxrs.JacksonJsonProvider;

import javax.inject.Singleton;
import javax.servlet.http.HttpServlet;
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

                configureJersey();

                configureJawr();
            }

            private void configureJawr() {
                // See https://groups.google.com/d/msg/google-guice/csiC0kiJLVg/93SOjg2qAJIJ

                bind(HttpServlet.class).annotatedWith(Names.named("jawr-js")).to(JawrServlet.class).in(Singleton.class);

                Map<String, String> initParams = new HashMap<>();
                initParams.put("configLocation", "/jawr.properties");
                initParams.put("type", "js");
                serve("*.js").with(Key.get(HttpServlet.class, Names.named("jawr-js")) , initParams);

                bind(HttpServlet.class).annotatedWith(Names.named("jawr-css")).to(JawrServlet.class).in(Singleton.class);

                initParams = new HashMap<>();
                initParams.put("configLocation", "/jawr.properties");
                initParams.put("type", "css");
                serve("*.css").with(Key.get(HttpServlet.class, Names.named("jawr-css")), initParams);
            }

            private void configureJackson() {
                JacksonJsonProvider jacksonJsonProvider = new JacksonJsonProvider()
                        .configure(JsonParser.Feature.ALLOW_SINGLE_QUOTES, true)
                        .configure(JsonParser.Feature.ALLOW_UNQUOTED_FIELD_NAMES, true);
                bind(JacksonJsonProvider.class).toInstance(jacksonJsonProvider);
            }

            private void configureJersey() {
                Map<String, String> initParams = new HashMap<>();
                initParams.put("com.sun.jersey.config.feature.Trace", "true");
                serve("/api/*").with(GuiceContainer.class, initParams);
            }
        });
    }
}
