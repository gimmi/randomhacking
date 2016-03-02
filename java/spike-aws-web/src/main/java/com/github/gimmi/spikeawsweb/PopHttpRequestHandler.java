package com.github.gimmi.spikeawsweb;

import com.amazonaws.services.sqs.AmazonSQS;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.HttpRequestHandler;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Optional;

public class PopHttpRequestHandler implements HttpRequestHandler {
   private static final Logger logger = LoggerFactory.getLogger(PopHttpRequestHandler.class);
   private final AmazonSQS sqs;
   private final MessageBus messageBus;
   private final DocRepository docRepository;

   public PopHttpRequestHandler(AmazonSQS sqs, MessageBus messageBus, DocRepository docRepository) {
      this.sqs = sqs;
      this.messageBus = messageBus;
      this.docRepository = docRepository;
   }

   @Override
   public void handleRequest(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
      Optional<String> doc = messageBus.consumeIncomingDoc(docRepository::get);

      if (doc.isPresent()) {
         response.setStatus(200);
         response.setContentType("text/plain");
         response.setCharacterEncoding("utf-8");
         response.getWriter().write(doc.get());
      } else {
         response.sendError(404, "Not Found");
      }
      response.flushBuffer();
   }
}
