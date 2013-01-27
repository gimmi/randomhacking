package com.github.gimmi.spikespringmvc;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import javax.sql.DataSource;

@Configuration
public class ContextConfiguration {
    @Bean
    public DataSource dataSource() {
        return null;
    }
}
