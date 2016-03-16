package com.github.gimmi.mvnspringhierarchy;

import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

public class TenantBeanFactory {
   private final ApplicationContext rootCtx;
   private final ConcurrentMap<String, ApplicationContext> cache;

   public TenantBeanFactory(ApplicationContext rootCtx) {
      this.rootCtx = rootCtx;
      cache = new ConcurrentHashMap<>();
   }

   public <T> T getBean(Tenant tenant, Class<T> requiredType) {
      return cache.computeIfAbsent(tenant.getId(), id -> buildCtx(tenant)).getBean(requiredType);
   }

   private ApplicationContext buildCtx(Tenant tenant) {
      AnnotationConfigApplicationContext ctx = new AnnotationConfigApplicationContext();
      ctx.setParent(rootCtx);
      ctx.register(TenantConfig.class);
      ctx.scan(tenant.getPkg());
      ctx.getBeanFactory().registerSingleton("tenant", tenant);
      ctx.refresh();
      ctx.registerShutdownHook();
      return ctx;
   }
}
