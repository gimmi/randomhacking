package com.github.gimmi.spikespringmvc;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
public class HomeController {
    final Logger logger = LoggerFactory.getLogger(HomeController.class);
    private final JdbcTemplate jdbcTemplate;

    public HomeController(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    @RequestMapping(value = "/")
    public String home() {
        logger.info("HomeController: Passing through...");
        return "/home.jsp";
    }
}
