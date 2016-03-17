package com.github.gimmi.mvnspringhierarchy;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.DisposableBean;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

public class TenantBeanFactory implements DisposableBean {
   private static final Logger logger = LoggerFactory.getLogger(TenantBeanFactory.class);

   private final ApplicationContext rootCtx;
   private final ConcurrentMap<String, AnnotationConfigApplicationContext> cache;

   public TenantBeanFactory(ApplicationContext rootCtx) {
      this.rootCtx = rootCtx;
      cache = new ConcurrentHashMap<>();
   }

   public <T> T getBean(Tenant tenant, Class<T> requiredType) {
      return cache.computeIfAbsent(tenant.getId(), id -> buildCtx(tenant)).getBean(requiredType);
   }

   private AnnotationConfigApplicationContext buildCtx(Tenant tenant) {
      logger.info("Creating context for tenant {}", tenant.getId());
      AnnotationConfigApplicationContext ctx = new AnnotationConfigApplicationContext();
      ctx.setParent(rootCtx);
      ctx.register(TenantConfig.class);
      ctx.scan(tenant.getPkg());
      ctx.getBeanFactory().registerSingleton("tenant", tenant);
      ctx.refresh();
      return ctx;
   }

   @Override
   public void destroy() throws Exception {
      cache.forEach((tenantId, ctx) -> {
         logger.info("Destroying context for tenant {}", tenantId);
         ctx.close();
      });
   }
}
