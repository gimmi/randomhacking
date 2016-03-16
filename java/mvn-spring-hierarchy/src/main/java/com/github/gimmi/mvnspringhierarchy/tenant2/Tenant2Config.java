package com.github.gimmi.mvnspringhierarchy.tenant2;

import com.github.gimmi.mvnspringhierarchy.Service;
import com.github.gimmi.mvnspringhierarchy.Tenant;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class Tenant2Config {
   @Bean
   public Tenant2Bean tenant2Bean() {
      return new Tenant2Bean();
   }

   @Bean
   public Service service(Tenant tenant) {
      return new Tenant2Service(tenant);
   }
}
