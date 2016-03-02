package com.github.gimmi.spikeawsweb;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.util.FileCopyUtils;
import org.springframework.web.HttpRequestHandler;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class PushHttpRequestHandler implements HttpRequestHandler {
   private static final Logger logger = LoggerFactory.getLogger(PushHttpRequestHandler.class);
   private final DocRepository docRepository;
   private final MessageBus messageBus;

   public PushHttpRequestHandler(DocRepository docRepository, MessageBus messageBus) {
      this.docRepository = docRepository;
      this.messageBus = messageBus;
   }

   @Override
   public void handleRequest(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
      if (!"POST".equals(request.getMethod())) {
         response.sendError(405, "Method Not Allowed");
         return;
      }

      if (!request.getContentType().startsWith("text/plain")) {
         response.sendError(406, "Not Acceptable");
         return;
      }

      logger.info("Handling request");

      String doc = FileCopyUtils.copyToString(request.getReader());

      String docId = docRepository.put(doc);

      messageBus.notifyIncomingDoc(docId);

      response.setStatus(201);
   }
}
