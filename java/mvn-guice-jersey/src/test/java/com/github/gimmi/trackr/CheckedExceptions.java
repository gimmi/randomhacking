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

        multilineString(
            "<html>",
            "  <body></body>",
            "</html>"
        );
    }

    public static String multilineString(String... strings) {
        String lineSeparator = System.getProperty("line.separator");
        StringBuilder sb = new StringBuilder();
        for (String string : strings) {
            sb.append(string).append(lineSeparator);
        }
        return sb.toString();
    }
}
