package com.github.gimmi.mvnjersey2;

import javax.inject.Singleton;

@Singleton
public class MySingletonScopedService {
    private static Integer i = 0;

    public MySingletonScopedService() {
        i++;
    }

    public String getString() {
        return String.format("Singleton instance #%d", i);
    }
}
