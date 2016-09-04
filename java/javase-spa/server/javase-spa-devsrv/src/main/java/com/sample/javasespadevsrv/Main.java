package com.sample.javasespadevsrv;

import com.sample.javasespa.JavasespaInitializer;
import io.undertow.Handlers;
import io.undertow.Undertow;
import io.undertow.server.handlers.PathHandler;
import io.undertow.servlet.Servlets;
import io.undertow.servlet.api.DeploymentInfo;
import io.undertow.servlet.api.DeploymentManager;
import io.undertow.servlet.api.ServletContainerInitializerInfo;
import org.springframework.web.SpringServletContainerInitializer;

import java.util.Collections;
import java.util.HashSet;

public class Main {
    private static final String HOST = "127.0.0.1";
    private static final int PORT = 8078;
    private static final String CONTEXT_PATH = "/";

    public static void main(String[] args) throws Exception {
        DeploymentInfo servletBuilder = Servlets.deployment()
                .setClassLoader(Main.class.getClassLoader())
                .setContextPath(CONTEXT_PATH)
                .setDeploymentName("app.war")
                .addServletContainerInitalizer(new ServletContainerInitializerInfo(SpringServletContainerInitializer.class, new HashSet<>(Collections.singletonList(JavasespaInitializer.class))));

        DeploymentManager deploymentManager = Servlets.defaultContainer().addDeployment(servletBuilder);
        deploymentManager.deploy();
        PathHandler pathHandler = Handlers.path(Handlers.redirect(CONTEXT_PATH)).addPrefixPath(CONTEXT_PATH, deploymentManager.start());

        Undertow server = Undertow.builder()
                .addHttpListener(PORT, HOST)
                .setHandler(pathHandler)
                .build();
        server.start();

        System.out.println("Listening on http://" + HOST + ":" + PORT + CONTEXT_PATH);
    }

}
