package com.github.gimmi.mvnspringhierarchy.tenant2;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class Tenant2Config {
   @Bean
   public Tenant2Bean tenant2Bean() {
      return new Tenant2Bean();
   }
}
