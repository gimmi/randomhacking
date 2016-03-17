package com.github.gimmi.mvnspringhierarchy;

import com.github.gimmi.mvnspringhierarchy.tenant1.Tenant1Bean;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.StrictAssertions.assertThatThrownBy;

public class ChildContextTest {

   private AnnotationConfigApplicationContext rootCtx;

   @Before
   public void before() {
      rootCtx = new AnnotationConfigApplicationContext(RootConfig.class);
   }

   @After
   public void after() {
      rootCtx.close();
   }

   @Test
   public void should_resolve_scoped_beans() {
      assertThat(rootCtx.getBean(Service.class)).isInstanceOf(RootService.class);
      assertThatThrownBy(() -> rootCtx.getBean(Tenant1Bean.class)).hasMessage("No qualifying bean of type [com.github.gimmi.mvnspringhierarchy.tenant1.Tenant1Bean] is defined");

      TenantBeanFactory factory = rootCtx.getBean(TenantBeanFactory.class);

      Tenant tenant1 = new Tenant("tenant 1", "com.github.gimmi.mvnspringhierarchy.tenant1");

      assertThat(factory.getBean(tenant1, RootBean.class)).isNotNull();
      assertThat(factory.getBean(tenant1, Tenant1Bean.class)).isNotNull();
      assertThat(factory.getBean(tenant1, Service.class)).isInstanceOf(TenantService.class);
      assertThat(factory.getBean(tenant1, Service.class)).hasToString("tenant 1");

      Tenant tenant2 = new Tenant("tenant 2", "com.github.gimmi.mvnspringhierarchy.tenant2");

      assertThatThrownBy(() -> factory.getBean(tenant2, Tenant1Bean.class)).hasMessage("No qualifying bean of type [com.github.gimmi.mvnspringhierarchy.tenant1.Tenant1Bean] is defined");
      assertThat(factory.getBean(tenant2, RootBean.class)).isNotNull();
      assertThat(factory.getBean(tenant2, Service.class)).isInstanceOf(TenantService.class);
      assertThat(factory.getBean(tenant2, Service.class)).hasToString("tenant 2");
   }
}
