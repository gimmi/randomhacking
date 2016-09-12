package com.sample.flywayoracle;

import oracle.jdbc.pool.OracleDataSource;
import org.flywaydb.core.Flyway;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Main {
    private static final Logger logger = LoggerFactory.getLogger(Main.class);

    public static void main(String[] args) throws Exception {
        logger.debug("Running");

        OracleDataSource dataSource = new OracleDataSource();
        dataSource.setURL("jdbc:oracle:thin:UNIT_TESTS/secret@localhost:1521:XE");

        Flyway flyway = new Flyway();
        flyway.setDataSource(dataSource);
        flyway.setLocations(Main.class.getPackage().getName());
        flyway.migrate();
    }
}
