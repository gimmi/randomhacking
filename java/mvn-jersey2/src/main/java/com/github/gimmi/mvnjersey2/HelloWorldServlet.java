package com.github.gimmi.mvnjersey2;

import java.io.IOException;
import java.io.PrintWriter;
import java.text.SimpleDateFormat;
import java.util.Date;
import javax.inject.Inject;
import javax.inject.Provider;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class HelloWorldServlet extends HttpServlet {

    @Inject
    MySingletonScopedService singletonScopedService;

    @Inject
    MyRequestScopedService requestScopedService;

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        response.setContentType("text/plain");
        try (final PrintWriter out = response.getWriter()) {
            out.println(new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").format(new Date()));
            out.println(singletonScopedService.getString());
            out.println(requestScopedService.getString());
            out.println("Hello world");
        }
    }
}
