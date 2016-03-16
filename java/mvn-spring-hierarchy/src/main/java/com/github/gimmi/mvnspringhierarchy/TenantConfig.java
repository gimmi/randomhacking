package com.github.gimmi.mvnspringhierarchy;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class TenantConfig {
   @Bean
   public Service service(Tenant tenant) {
      return new TenantService(tenant);
   }
}
