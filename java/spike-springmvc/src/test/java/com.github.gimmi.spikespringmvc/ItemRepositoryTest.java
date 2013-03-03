package com.github.gimmi.spikespringmvc;

import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.datasource.embedded.EmbeddedDatabase;
import org.springframework.jdbc.datasource.embedded.EmbeddedDatabaseBuilder;
import org.springframework.jdbc.datasource.embedded.EmbeddedDatabaseType;
import org.springframework.web.context.WebApplicationContext;
import org.springframework.web.context.support.XmlWebApplicationContext;

import java.util.UUID;

public class ItemRepositoryTest {

    private ItemRepository sut;
    private EmbeddedDatabase db;

    @Before
    public void before() {
        db = new EmbeddedDatabaseBuilder()
                .setType(EmbeddedDatabaseType.H2)
                .addDefaultScripts()
                .build();

        sut = new ItemRepository(db);
    }

    @After
    public void after() {
        db.shutdown();
	    new XmlWebApplicationContext();
    }

    @Test
    public void should_insert_and_retrieve_item() {
        Item item = new Item();
        item.setTitle("title");
        item.setBody("body");
        sut.save(item);

        Item actualItem = sut.get(item.getId());
        Assert.assertEquals(item.getId(), actualItem.getId());
        Assert.assertEquals(item.getTitle(), actualItem.getTitle());
        Assert.assertEquals(item.getBody(), actualItem.getBody());
    }
}
