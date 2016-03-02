package com.github.gimmi.spikeawsweb;

import com.amazonaws.regions.Region;
import com.amazonaws.regions.Regions;
import com.amazonaws.services.s3.AmazonS3;
import com.amazonaws.services.s3.AmazonS3Client;
import com.amazonaws.services.sqs.AmazonSQS;
import com.amazonaws.services.sqs.AmazonSQSClient;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.PropertySource;
import org.springframework.core.env.Environment;

@Configuration
@PropertySource(value = {"classpath:com/github/gimmi/spikeawsweb/config.properties", "file:${com.github.gimmi.spikeawsweb.ConfigFile}"}, ignoreResourceNotFound = true)
public class WebApiConfig {
   private static final Logger logger = LoggerFactory.getLogger(WebApiConfig.class);

   @Autowired
   Environment env;

   @Bean
   public PushHttpRequestHandler pushHttpRequestHandler() {
      return new PushHttpRequestHandler(docRepository(), messagePublisher());
   }

   @Bean
   public PopHttpRequestHandler popHttpRequestHandler() {
      return new PopHttpRequestHandler(messagePublisher(), docRepository());
   }

   @Bean
   public AmazonS3 amazonS3() {
      String regionName = "eu-west-1";

      AmazonS3Client s3 = new AmazonS3Client();
      s3.setRegion(Region.getRegion(Regions.fromName(regionName)));
      return s3;
   }

   @Bean
   public AmazonSQS amazonSQS() {
      String regionName = "eu-west-1";

      AmazonSQSClient sqs = new AmazonSQSClient();
      sqs.setRegion(Region.getRegion(Regions.fromName(regionName)));
      return sqs;
   }

   @Bean
   public DocRepository docRepository() {
      String bucketName = "com.github.gimmi.docs";

      return new DocRepository(amazonS3(), bucketName);
   }

   @Bean
   public MessageBus messagePublisher() {
      return new MessageBus(amazonSQS(), gson());
   }

   @Bean
   public Gson gson() {
      GsonBuilder gsonBuilder = new GsonBuilder();
      return gsonBuilder.setPrettyPrinting().create();
   }
}
