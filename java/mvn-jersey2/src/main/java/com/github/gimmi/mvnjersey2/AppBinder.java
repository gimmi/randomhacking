package com.github.gimmi.mvnjersey2;

import javax.inject.Singleton;
import org.glassfish.hk2.utilities.binding.AbstractBinder;
import org.glassfish.jersey.process.internal.RequestScoped;

public class AppBinder extends AbstractBinder {

    @Override
    protected void configure() {
        bind(MyRequestScopedService.class).to(MyRequestScopedService.class).in(RequestScoped.class);
        bind(MySingletonScopedService.class).to(MySingletonScopedService.class).in(Singleton.class);
    }

}
