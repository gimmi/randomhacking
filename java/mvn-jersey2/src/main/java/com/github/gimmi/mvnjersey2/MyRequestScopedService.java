package com.github.gimmi.mvnjersey2;

public class MyRequestScopedService {

    private Integer i = 0;

    public String getString() {
        return String.format("RequestScoped call count: %d", ++i);
    }

}
