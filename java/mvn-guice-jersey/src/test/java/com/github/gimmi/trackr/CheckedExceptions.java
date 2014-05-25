package com.github.gimmi.trackr;

public class CheckedExceptions {
    public static interface Action {
        void invoke() throws Exception;
    }

    public static void unchecked(Action action) {
        try {
            action.invoke();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}
