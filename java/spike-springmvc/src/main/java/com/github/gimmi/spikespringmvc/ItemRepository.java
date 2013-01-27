package com.github.gimmi.spikespringmvc;

import org.springframework.jdbc.core.RowMapper;
import org.springframework.jdbc.core.namedparam.BeanPropertySqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;

import javax.sql.DataSource;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Collections;

public class ItemRepository {

    private final NamedParameterJdbcTemplate tpl;

    public ItemRepository(DataSource dataSource) {
        tpl = new NamedParameterJdbcTemplate(dataSource);
    }

    public void save(Item item) {
        tpl.update("INSERT INTO Items(Id, Title, Body) VALUES(:id, :title, :body)", new BeanPropertySqlParameterSource(item));
    }

    public Item get(String id){
        return tpl.queryForObject("SELECT * FROM Items WHERE Id = :id", Collections.singletonMap("id", id), new ItemMapper());
    }

    private static final class ItemMapper implements RowMapper<Item> {
        public Item mapRow(ResultSet rs, int rowNum) throws SQLException {
            Item actor = new Item(rs.getString("Id"));
            actor.setTitle(rs.getString("Title"));
            actor.setBody(rs.getString("Body"));
            return actor;
        }
    }
}
