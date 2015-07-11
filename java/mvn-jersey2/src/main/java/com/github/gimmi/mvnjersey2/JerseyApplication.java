package com.github.gimmi.mvnjersey2;

import org.glassfish.jersey.server.ResourceConfig;
import org.glassfish.jersey.server.ServerProperties;

public class JerseyApplication extends ResourceConfig {

    public JerseyApplication() {
        packages("com.github.gimmi.mvnjersey2");
        //register(MoxyJsonFeature.class);
        register(new AppBinder());

        // Enable for debug
        property(ServerProperties.TRACING, "ALL");
        property(ServerProperties.TRACING_THRESHOLD, "VERBOSE");
        property(ServerProperties.WADL_FEATURE_DISABLE, "true");
        
        
    }

}
