package com.github.gimmi.trackr.domain;

import com.github.gimmi.trackr.TestDbHelpers;
import org.eclipse.persistence.config.PersistenceUnitProperties;
import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Properties;

import static org.hamcrest.Matchers.equalTo;
import static org.hamcrest.Matchers.hasItems;
import static org.junit.Assert.assertThat;

public class ItemTest {
    private EntityManagerFactory emf;

    @Before
    public void before() {
        TestDbHelpers.rebuildDatabase();
        emf = TestDbHelpers.createEntityManagerFactory();
    }

    @After
    public void after() {
        emf.close();
    }

    @Test
    public void should_save_then_retrieve() {
        EntityManager entityManager = emf.createEntityManager();
        entityManager.getTransaction().begin();
        Item task = new Item();
        task.setTitle("Our very first task!");
        entityManager.persist(task);
        entityManager.getTransaction().commit();
        entityManager.close();

        entityManager = emf.createEntityManager();
        entityManager.getTransaction().begin();
        List<Item> result = entityManager.createQuery("SELECT i FROM Item i", Item.class).getResultList();
        Assert.assertEquals(1, result.size());
        Assert.assertEquals("Our very first task!", result.get(0).getTitle());
        Assert.assertEquals(task.getId(), result.get(0).getId());
        entityManager.getTransaction().commit();
        entityManager.close();
    }

    @Test
    public void should_handle_version_field() {
        Item task = new Item();
        assertThat(task.getVersion(), equalTo(0));

        EntityManager entityManager = emf.createEntityManager();
        entityManager.getTransaction().begin();
        entityManager.persist(task);
        Assert.assertEquals(0, task.getVersion());
        entityManager.getTransaction().commit();
        entityManager.close();

        assertThat(task.getVersion(), equalTo(1));
    }

    @Test
    public void should_handle_tags() {
        Item item;

        EntityManager entityManager = emf.createEntityManager();
        entityManager.getTransaction().begin();
        item = new Item();
        item.getTags().add(new Tag("tag1"));
        item.getTags().add(new Tag("tag2"));
        entityManager.persist(item);
        entityManager.getTransaction().commit();
        entityManager.close();

        entityManager = emf.createEntityManager();
	    entityManager.getTransaction().begin();
        item = entityManager.find(Item.class, item.getId());
	    assertThat(item.getTags().size(), equalTo(2));
	    assertThat(item.getTags(), hasItems(new Tag("tag1"), new Tag("tag2")));
	    item.getTags().remove(new Tag("tag1"));
	    item.getTags().add(new Tag("tag3"));
	    entityManager.getTransaction().commit();
	    entityManager.close();

        entityManager = emf.createEntityManager();
	    entityManager.getTransaction().begin();
        item = entityManager.find(Item.class, item.getId());
	    assertThat(item.getTags().size(), equalTo(2));
	    assertThat(item.getTags(), hasItems(new Tag("tag1"), new Tag("tag2")));
	    entityManager.getTransaction().commit();
	    entityManager.close();
    }
}
