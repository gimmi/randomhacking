package com.github.gimmi.mvnjersey2;

import javax.enterprise.context.RequestScoped;

@RequestScoped
public class MyRequestScopedService {
    private static Integer i = 0;

    public MyRequestScopedService() {
        i++;
    }

    public String getString() {
        return String.format("Request instance #%d", i);
    }
}
