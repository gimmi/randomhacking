package com.github.gimmi.spikeawsworker;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.util.FileCopyUtils;
import org.springframework.web.HttpRequestHandler;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Enumeration;

public class ProcessHttpRequestHandler implements HttpRequestHandler {
   private static final Logger logger = LoggerFactory.getLogger(ProcessHttpRequestHandler.class);

   @Override
   public void handleRequest(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
      if (!"POST".equals(request.getMethod())) {
         response.sendError(405, "Method Not Allowed");
         return;
      }

      Enumeration<String> names = request.getHeaderNames();
      while (names.hasMoreElements()) {
         String name = names.nextElement();
         logger.info("{}: {}", name, request.getHeader(name));
      }
      logger.info(FileCopyUtils.copyToString(request.getReader()));

      response.setStatus(200);
   }
}
