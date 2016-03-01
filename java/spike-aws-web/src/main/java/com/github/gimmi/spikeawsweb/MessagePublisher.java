package com.github.gimmi.spikeawsweb;

import com.amazonaws.services.sqs.AmazonSQS;
import com.amazonaws.services.sqs.model.MessageAttributeValue;
import com.amazonaws.services.sqs.model.SendMessageRequest;
import com.amazonaws.services.sqs.model.SendMessageResult;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class MessagePublisher {
   private static final Logger logger = LoggerFactory.getLogger(PushHttpRequestHandler.class);
   private final AmazonSQS sqs;
   private final Gson gson;

   public MessagePublisher(AmazonSQS sqs, Gson gson) {
      this.sqs = sqs;
      this.gson = gson;
   }

   public void notifyIncomingDoc(String docId) {
      JsonObject json = new JsonObject();
      json.addProperty("docId", docId);
      SendMessageResult result = sqs.sendMessage(new SendMessageRequest()
         .withQueueUrl("incoming-doc")
         .addMessageAttributesEntry("Content-Type", new MessageAttributeValue().withDataType("String").withStringValue("application/json"))
         .withMessageBody(gson.toJson(json))
      );

      logger.info("Published message {} (MD5: {})", result.getMessageId(), result.getMD5OfMessageBody());
   }
}
