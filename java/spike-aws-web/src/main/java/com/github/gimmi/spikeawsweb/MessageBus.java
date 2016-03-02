package com.github.gimmi.spikeawsweb;

import com.amazonaws.services.sqs.AmazonSQS;
import com.amazonaws.services.sqs.model.*;
import com.google.gson.Gson;
import com.google.gson.JsonObject;
import jdk.nashorn.internal.runtime.options.Option;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.List;
import java.util.Optional;
import java.util.function.Consumer;
import java.util.function.Function;

public class MessageBus {
   private static final Logger logger = LoggerFactory.getLogger(PushHttpRequestHandler.class);
   private final AmazonSQS sqs;
   private final Gson gson;

   public MessageBus(AmazonSQS sqs, Gson gson) {
      this.sqs = sqs;
      this.gson = gson;
   }

   public void notifyIncomingDoc(String docId) {
      JsonObject json = new JsonObject();
      json.addProperty("docId", docId);
      SendMessageResult result = sqs.sendMessage(new SendMessageRequest()
         .withQueueUrl("incoming-docs")
         .addMessageAttributesEntry("Content-Type", new MessageAttributeValue().withDataType("String").withStringValue("application/json"))
         .withMessageBody(gson.toJson(json))
      );

      logger.info("Published message {} (MD5: {})", result.getMessageId(), result.getMD5OfMessageBody());
   }

   public <T> Optional<T> consumeIncomingDoc(Function<String, T> consumer) {
      String queueUrl = "incoming-docs";

      List<Message> messages = sqs.receiveMessage(new ReceiveMessageRequest(queueUrl)
         .withMaxNumberOfMessages(1)
         .withVisibilityTimeout(60)).getMessages();

      if (messages.isEmpty()) {
         return Optional.empty();
      }

      Message message = messages.get(0);

      String docId = gson.fromJson(message.getBody(), JsonObject.class).get("docId").getAsString();
      T ret = consumer.apply(docId);

      sqs.deleteMessage(queueUrl, message.getReceiptHandle());

      return Optional.of(ret);
   }
}
