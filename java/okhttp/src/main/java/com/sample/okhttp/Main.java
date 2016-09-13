package com.sample.okhttp;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import okhttp3.*;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Main {
    private static final Logger logger = LoggerFactory.getLogger(Main.class);

    public static void main(String[] args) throws Exception {
        logger.debug("Running");

        Gson gson = new Gson();

        MediaType jsonMediaType = MediaType.parse("application/json; charset=utf-8");
        Request request = new Request.Builder()
                .url("https://echo.getpostman.com/post")
                .post(RequestBody.create(jsonMediaType, "[1,2,3]"))
                .build();

        // TODO this must be a singleton
        OkHttpClient client = new OkHttpClient.Builder()
                .build();

        Response response = client.newCall(request).execute();

        JsonObject body = gson.fromJson(response.body().charStream(), JsonObject.class);

        System.out.println(body);
    }
}
