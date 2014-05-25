package com.github.gimmi.trackr;

import com.github.gimmi.trackr.configuration.AppServletContextListener;
import com.google.inject.servlet.GuiceFilter;
import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.servlet.DefaultServlet;
import org.eclipse.jetty.servlet.FilterMapping;
import org.eclipse.jetty.servlet.ServletContextHandler;

import static com.github.gimmi.trackr.CheckedExceptions.unchecked;

public class TestServerHelpers {
    public static Server buildWebServer() {
        Server server = new Server(8080);

        ServletContextHandler servletContextHandler = new ServletContextHandler(ServletContextHandler.SESSIONS);
        servletContextHandler.setContextPath("/");
        servletContextHandler.addEventListener(new AppServletContextListener());
        servletContextHandler.addFilter(GuiceFilter.class, "/*", FilterMapping.DEFAULT);

        // This is a workaround
        // https://groups.google.com/d/msg/google-guice/lMmVPQQ2Soc/0QoekenBnYoJ
        // https://bugs.eclipse.org/bugs/show_bug.cgi?id=393738
        servletContextHandler.addServlet(DefaultServlet.class, "/");

        server.setHandler(servletContextHandler);

        unchecked(server::start);

        return server;
    }

}
