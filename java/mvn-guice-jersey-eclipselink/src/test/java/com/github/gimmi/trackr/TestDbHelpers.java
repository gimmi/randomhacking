package com.github.gimmi.trackr;

import org.eclipse.persistence.config.PersistenceUnitProperties;

import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import java.sql.*;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class TestDbHelpers {
	public static final String CONNSTR = "jdbc:h2:mem:db1;DB_CLOSE_DELAY=-1;MVCC=TRUE";
	public static final String USER = "sa";
	public static final String PWD = "";
	public static final String DRIVER = "org.h2.Driver";
	public static final String PERSISTENCE_UNIT_NAME = "com.github.gimmi.trackr";

	public static void createDdlSqlFile() {
		Map<String, String> map = getTestJpaProps();

		// See http://wiki.eclipse.org/EclipseLink/Examples/JPA/DDL
		// See http://eclipse.org/eclipselink/documentation/2.4/jpa/extensions/p_ddl_generation.htm
		map.put(PersistenceUnitProperties.DDL_GENERATION, PersistenceUnitProperties.CREATE_ONLY);
		map.put(PersistenceUnitProperties.DDL_GENERATION_MODE, PersistenceUnitProperties.DDL_SQL_SCRIPT_GENERATION);
		map.put(PersistenceUnitProperties.CREATE_JDBC_DDL_FILE, "create.sql");
		map.put(PersistenceUnitProperties.APP_LOCATION, "c:\\users\\gimmi\\temp");

		Persistence.createEntityManagerFactory(PERSISTENCE_UNIT_NAME, map).createEntityManager().close();
	}

	public static EntityManagerFactory createEntityManagerFactory() {
		return Persistence.createEntityManagerFactory(PERSISTENCE_UNIT_NAME, getTestJpaProps());
	}

	public static Map<String, String> getTestJpaProps() {
		Map<String, String> map = new HashMap<>();
		map.put("javax.persistence.jdbc.driver", DRIVER);
		map.put("javax.persistence.jdbc.url", CONNSTR);
		map.put("javax.persistence.jdbc.user", USER);
		map.put("javax.persistence.jdbc.password", PWD);

		// See http://wiki.eclipse.org/EclipseLink/Examples/JPA/Logging
		map.put("eclipselink.logging.logger", "DefaultLogger");
		map.put("eclipselink.logging.level", "ALL");

		map.put(PersistenceUnitProperties.DDL_GENERATION, PersistenceUnitProperties.NONE);

		return map;
	}

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
