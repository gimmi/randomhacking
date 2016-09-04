package com.sample.javasespa;

import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HelloController {
    @RequestMapping(method = RequestMethod.GET, path = "/api/hello", produces = MediaType.TEXT_PLAIN_VALUE)
    public String getHello(@RequestParam(value = "subject", defaultValue = "World") String subject) {
        return "Hello " + subject;
    }
}
