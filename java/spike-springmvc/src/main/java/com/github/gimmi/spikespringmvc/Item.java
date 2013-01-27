package com.github.gimmi.spikespringmvc;

import java.util.UUID;

public class Item {
    private final String id;
    private String title;
    private String body;

    public Item() {
        this(UUID.randomUUID().toString());
    }

    public Item(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getBody() {
        return body;
    }

    public void setBody(String body) {
        this.body = body;
    }
}
