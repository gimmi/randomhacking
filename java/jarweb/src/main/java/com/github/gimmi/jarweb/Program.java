package com.github.gimmi.jarweb;

import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.server.ServerConnector;
import org.eclipse.jetty.servlet.ServletHandler;

public class Program {

    public static void main(String[] args) throws Exception {
        Server server = new Server();

        ServerConnector http = new ServerConnector(server);
        http.setPort(8080);
        server.addConnector(http);

        ServletHandler handler = new ServletHandler();
        server.setHandler(handler);
        handler.addServletWithMapping(HelloServlet.class, "/*");

        server.start();
        server.dumpStdErr();
        server.join();
    }
}
