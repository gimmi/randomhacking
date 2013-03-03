package com.github.gimmi.spikespringmvc;


import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

@Component
@Scope("prototype")
public class MyComponent {
	final Logger logger = LoggerFactory.getLogger(ItemsController.class);

	public MyComponent() {
		logger.info("MyComponent: ctor");
	}
}
