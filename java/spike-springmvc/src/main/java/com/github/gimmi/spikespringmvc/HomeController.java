package com.github.gimmi.spikespringmvc;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Scope;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@Scope("prototype")
public class HomeController {
    final Logger logger = LoggerFactory.getLogger(HomeController.class);
	private final MyComponent myComponent;

	@Autowired
    public HomeController(MyComponent myComponent) {
		this.myComponent = myComponent;
		logger.info("HomeController: ctor");
    }

    @RequestMapping(value = "/")
    public String home() {
        logger.info("HomeController: Passing through...");
        return "/home.jsp";
    }
}
