package com.sample.javasespadevsrv;

import com.sample.javasespa.HelloServlet;
import io.undertow.Handlers;
import io.undertow.Undertow;
import io.undertow.server.handlers.PathHandler;
import io.undertow.servlet.Servlets;
import io.undertow.servlet.api.DeploymentInfo;
import io.undertow.servlet.api.DeploymentManager;

public class Main {
    public static final int PORT = 8078;
    public static final String HOST = "127.0.0.1";
    public static final String CONTEXT_PATH = "/";

    public static void main(String[] args) throws Exception {
        DeploymentInfo servletBuilder = Servlets.deployment()
                .setClassLoader(Main.class.getClassLoader())
                .setContextPath(CONTEXT_PATH)
                .setDeploymentName("javasespadevsrv.war")
                .addServlets(
                        Servlets.servlet("HelloServlet", HelloServlet.class).addMapping("/hello")
                );

        DeploymentManager manager = Servlets.defaultContainer().addDeployment(servletBuilder);
        manager.deploy();
        PathHandler path = Handlers.path(Handlers.redirect(CONTEXT_PATH)).addPrefixPath(CONTEXT_PATH, manager.start());

        Undertow server = Undertow.builder()
                .addHttpListener(PORT, HOST) // TODO for ssl see http://stackoverflow.com/a/26603219
                .setHandler(path)
                .build();
        server.start();

        System.out.println("Listening on http://" + HOST + ":" + PORT + CONTEXT_PATH);
    }
}
