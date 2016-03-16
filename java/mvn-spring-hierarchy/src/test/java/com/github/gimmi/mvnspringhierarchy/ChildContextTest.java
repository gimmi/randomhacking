package com.github.gimmi.mvnspringhierarchy;

import com.github.gimmi.mvnspringhierarchy.tenant1.Tenant1Bean;
import com.github.gimmi.mvnspringhierarchy.tenant1.Tenant1Service;
import org.junit.Test;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.StrictAssertions.assertThatThrownBy;

public class ChildContextTest {
   @Test
   public void should_resolve_scoped_beans() {
      AnnotationConfigApplicationContext rootCtx = new AnnotationConfigApplicationContext(RootConfig.class);

      AnnotationConfigApplicationContext childCtx = new AnnotationConfigApplicationContext();
      childCtx.setParent(rootCtx);
      childCtx.scan("com.github.gimmi.mvnspringhierarchy.tenant1");
      childCtx.refresh();

      assertThatThrownBy(() -> rootCtx.getBean(Tenant1Bean.class)).hasMessage("No qualifying bean of type [com.github.gimmi.mvnspringhierarchy.tenant1.Tenant1Bean] is defined");
      assertThat(childCtx.getBean(RootBean.class)).isNotNull();
      assertThat(childCtx.getBean(Tenant1Bean.class)).isNotNull();
      assertThat(childCtx.getBean(Service.class)).isInstanceOf(Tenant1Service.class);
      assertThat(rootCtx.getBean(Service.class)).isInstanceOf(RootService.class);
   }
}
