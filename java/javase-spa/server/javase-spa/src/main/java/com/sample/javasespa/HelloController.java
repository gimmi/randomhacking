package com.sample.javasespa;

import com.google.gson.JsonObject;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HelloController {
    private static final Logger logger = LoggerFactory.getLogger(HelloController.class);

    @RequestMapping(method = RequestMethod.GET, path = "/hello", produces = MediaType.APPLICATION_JSON_UTF8_VALUE)
    public JsonObject getHello(@RequestParam(value = "subject", defaultValue = "World") String subject) {
        logger.info("Invoking method");

        JsonObject ret = new JsonObject();
        ret.addProperty("message", "Hello " + subject);

        return ret;
    }
}
