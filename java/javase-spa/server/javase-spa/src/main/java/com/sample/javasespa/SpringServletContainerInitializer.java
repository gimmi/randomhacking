package com.sample.javasespa;

import javax.servlet.ServletContainerInitializer;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.annotation.HandlesTypes;
import java.util.Set;

// This simulate org.springframework.web.SpringServletContainerInitializer
@HandlesTypes({WebApplicationInitializerImpl.class})
public class SpringServletContainerInitializer implements ServletContainerInitializer {
    @Override
    public void onStartup(Set<Class<?>> classes, ServletContext ctx) throws ServletException {
        for (Class<?> clazz : classes) {
            try {
                WebApplicationInitializerImpl webApplicationInitializerImpl = (WebApplicationInitializerImpl) clazz.newInstance();
                webApplicationInitializerImpl.init(ctx);
            } catch (Exception e) {
                throw new ServletException(e);
            }
        }
    }
}
