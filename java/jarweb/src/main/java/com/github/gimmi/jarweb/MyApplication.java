package com.github.gimmi.jarweb;

import javax.json.stream.JsonGenerator;
import org.glassfish.hk2.utilities.binding.AbstractBinder;
import org.glassfish.jersey.process.internal.RequestScoped;
import org.glassfish.jersey.server.ResourceConfig;

public class MyApplication extends ResourceConfig {

    public MyApplication() {
        register(MyResource.class);
        
        // register(org.glassfish.jersey.jsonp.JsonProcessingFeature.class);
        property(JsonGenerator.PRETTY_PRINTING, true);
        
        register(new AbstractBinder() {
            @Override
            protected void configure() {
                bind(MyObject.class).to(MyObject.class).in(RequestScoped.class);
            }
        });
    }
}
