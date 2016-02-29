package com.github.gimmi.spikeawsweb;

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
   public HelloHttpRequestHandler helloHttpRequestHandler() {
      return new HelloHttpRequestHandler();
   }
}
