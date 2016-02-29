package com.github.gimmi.spikeawsweb;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.HttpRequestHandler;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class HelloHttpRequestHandler implements HttpRequestHandler {
   private static final Logger logger = LoggerFactory.getLogger(HelloHttpRequestHandler.class);

   @Override
   public void handleRequest(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
      if (!"GET".equals(request.getMethod())) {
         response.sendError(405, "Method Not Allowed");
         return;
      }

      logger.info("Handling request");

      response.setStatus(202);
      response.setContentType("text/plain");
      response.getWriter().write("Hello world");
      response.flushBuffer();
   }
}
