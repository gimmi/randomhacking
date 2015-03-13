package com.github.gimmi.jarweb;

import org.glassfish.hk2.utilities.binding.AbstractBinder;
import org.glassfish.jersey.server.ResourceConfig;

public class MyApplication extends ResourceConfig {

    public MyApplication() {
        packages("com.github.gimmi.jarweb");
        register(new AbstractBinder() {
            @Override
            protected void configure() {
                bind(MyObject.class).to(MyObject.class);
            }
        });
    }
}
