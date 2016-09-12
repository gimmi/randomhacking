package com.sample.jsonpath;

import com.google.gson.JsonObject;
import com.jayway.jsonpath.JsonPath;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Main {
    private static final Logger logger = LoggerFactory.getLogger(Main.class);

    public static void main(String[] args) throws Exception {
        logger.debug("Running");

        com.jayway.jsonpath.Configuration.setDefaults(new JsonpathDefaults());

        JsonObject obj = new JsonObject();
        obj.addProperty("prop", "value");

        String value = JsonPath.parse(obj).read("$.prop", String.class);

        logger.info("Value: {}", value);
    }
}
