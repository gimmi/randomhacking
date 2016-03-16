package com.github.gimmi.mvnspringhierarchy;

import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class RootConfig {
   @Bean
   public RootBean rootBean() {
      return new RootBean();
   }

   @Bean
   public Service service() {
      return new RootService();
   }

   @Bean
   public TenantBeanFactory tenantManager(ApplicationContext applicationContext) {
      return  new TenantBeanFactory(applicationContext);
   }
}
