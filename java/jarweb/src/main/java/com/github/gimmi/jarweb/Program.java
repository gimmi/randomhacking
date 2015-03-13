package com.github.gimmi.jarweb;

import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.servlet.ServletContextHandler;
import org.eclipse.jetty.servlet.ServletHolder;
import org.glassfish.jersey.servlet.ServletContainer;

public class Program {

    public static void main(String[] args) throws Exception {

        ServletContextHandler sch = new ServletContextHandler(ServletContextHandler.SESSIONS);
        sch.setContextPath("/app");

        ServletHolder sh = new ServletHolder(ServletContainer.class);
        sh.setInitParameter("jersey.config.server.provider.packages", "com.github.gimmi.jarweb");
        sh.setInitOrder(1);
        sch.addServlet(sh, "/webapi/*");
        sch.addServlet(HelloServlet.class, "/hello");

        Server server = new Server(8080);
        server.setHandler(sch);
        server.start();
        server.dumpStdErr();
        server.join();
    }
}
