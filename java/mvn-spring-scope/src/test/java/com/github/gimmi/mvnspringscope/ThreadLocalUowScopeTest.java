package com.github.gimmi.mvnspringscope;

import org.junit.Test;
import org.springframework.beans.factory.config.CustomScopeConfigurer;
import org.springframework.context.annotation.*;

import static org.assertj.core.api.Assertions.assertThat;

public class ThreadLocalUowScopeTest {

	@Configuration
	public static class TestConfig {
		@Bean
		public static CustomScopeConfigurer customScopeConfigurer() {
			CustomScopeConfigurer csg = new CustomScopeConfigurer();
			csg.addScope("company", new ThreadLocalUowScope());
			return csg;
		}

		@Bean
		public MyBean singletonBean() {
			return new MyBean("singleton", companyBean());
		}

		@Bean
		@Scope("prototype")
		public MyBean prototypeBean() {
			return new MyBean("prototype", companyBean());
		}

		@Bean
		@Scope(scopeName = "company", proxyMode = ScopedProxyMode.TARGET_CLASS)
		public MyBean companyBean() {
			return new MyBean("company", null);
		}
	}

	public static class MyBean {
		private String id;
		private MyBean inner;

		public MyBean(String id, MyBean inner) {
			this.id = id;
			this.inner = inner;
		}

		public String getId() {
			return id;
		}

		public MyBean getInner() {
			return inner;
		}
	}

	@Test
	public void should_resolve_scoped_beans() {
		AnnotationConfigApplicationContext ctx = new AnnotationConfigApplicationContext(TestConfig.class);

		ThreadLocalUowScope.begin();

		MyBean singletonBean = ctx.getBean("singletonBean", MyBean.class);
		assertThat(singletonBean).isNotNull();
		assertThat(singletonBean).isSameAs(ctx.getBean("singletonBean", MyBean.class));
		assertThat(singletonBean.getId()).isEqualTo("singleton");
		assertThat(singletonBean.getInner().getId()).isEqualTo("company");

		MyBean prototypeBean = ctx.getBean("prototypeBean", MyBean.class);
		assertThat(prototypeBean).isNotNull();

		MyBean companyBean = ctx.getBean("companyBean", MyBean.class);
		assertThat(companyBean).isNotNull();

		ThreadLocalUowScope.end();
	}
}
