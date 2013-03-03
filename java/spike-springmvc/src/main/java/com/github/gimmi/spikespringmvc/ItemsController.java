package com.github.gimmi.spikespringmvc;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@Controller
@Scope("prototype")
public class ItemsController {
	final Logger logger = LoggerFactory.getLogger(ItemsController.class);
	private final MyComponent myComponent;

	@Autowired
	public ItemsController(MyComponent myComponent) {
		this.myComponent = myComponent;
		logger.info("ItemsController: ctor");
	}

	@RequestMapping(value = "/items/{id}", method = RequestMethod.GET)
	@ResponseBody
	public Item get(@PathVariable("id") String id) {
		logger.info("ItemsController: GET id={}", id);
		return new Item();
	}

	@RequestMapping(value = "/items", method = RequestMethod.POST)
	public void post(@PathVariable("id") String id, @RequestBody Item item) {
		logger.info("ItemsController: GET id={}", id);
	}
}
