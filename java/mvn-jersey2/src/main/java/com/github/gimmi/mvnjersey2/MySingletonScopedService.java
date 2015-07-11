package com.github.gimmi.mvnjersey2;

public class MySingletonScopedService {
    private Integer i = 0;

    public String getString() {
        return String.format("Singleton call count: %d", ++i);
    }
}
