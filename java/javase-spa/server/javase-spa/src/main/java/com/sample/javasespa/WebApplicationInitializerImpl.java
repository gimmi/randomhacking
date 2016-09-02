package com.sample.javasespa;

import com.sample.javasespa.HelloServlet;

import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.ServletRegistration;

// This simulate implementations of org.springframework.web.WebApplicationInitializer
public class WebApplicationInitializerImpl {
    public void init(ServletContext servletContext) throws ServletException {
        servletContext.addServlet("HelloServlet", HelloServlet.class).addMapping("/hello");
    }
}
