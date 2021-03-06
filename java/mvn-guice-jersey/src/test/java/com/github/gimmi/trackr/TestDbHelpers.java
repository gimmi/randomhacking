package com.github.gimmi.trackr;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class TestDbHelpers {
    public static final String CONNSTR = "jdbc:h2:mem:db1;DB_CLOSE_DELAY=-1;MVCC=TRUE";
    public static final String USER = "sa";
    public static final String PWD = "";
    public static final String DRIVER = "org.h2.Driver";

    public static void rebuildDatabase() {
        execSql("DROP TABLE IF EXISTS TAGS");
        execSql("DROP TABLE IF EXISTS ITEMS");
        execSql("CREATE TABLE ITEMS (ID CHAR(36), TITLE VARCHAR, VERSION INTEGER, PRIMARY KEY (ID))");
        execSql("CREATE TABLE TAGS (ITEM_ID CHAR(36), TAG VARCHAR)");
        execSql("ALTER TABLE TAGS ADD CONSTRAINT UNQ_TAGS_0 UNIQUE (ITEM_ID, TAG)");
        execSql("ALTER TABLE TAGS ADD CONSTRAINT FK_TAGS_ITEM_ID FOREIGN KEY (ITEM_ID) REFERENCES ITEMS (ID)");
    }

    public static void execSql(String sql) {
        try {
            Class.forName(DRIVER);
            Connection conn = DriverManager.getConnection(CONNSTR, USER, PWD);
            conn.createStatement().execute(sql);
            conn.close();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
        String[] lines = {
                "Line 1",
                "Line 2"
        };
    }

    public static List<Map<String, Object>> query(String sql) {
        try {
            Class.forName(DRIVER);
            Connection conn = DriverManager.getConnection(CONNSTR, USER, PWD);
            ResultSet rs = conn.createStatement().executeQuery(sql);
            ArrayList<Map<String, Object>> rows = new ArrayList<>();
            while (rs.next()) {
                ResultSetMetaData rsmd = rs.getMetaData();

                Map<String, Object> row = new HashMap<>();
                for (int i = 1; i <= rsmd.getColumnCount(); i++) {
                    row.put(rsmd.getColumnName(i), rs.getObject(i));
                }

                rows.add(row);
            }
            rs.close();
            conn.close();
            return rows;
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}
