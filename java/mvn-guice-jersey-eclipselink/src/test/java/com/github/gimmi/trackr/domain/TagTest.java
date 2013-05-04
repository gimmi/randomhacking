package com.github.gimmi.trackr.domain;

import com.github.gimmi.trackr.TestDbHelpers;
import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import java.util.List;

import static org.hamcrest.Matchers.equalTo;
import static org.hamcrest.Matchers.hasItems;
import static org.junit.Assert.assertThat;

public class TagTest {
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
		Tag tag = new Tag();
		tag.setName("Our very first tag!");
		entityManager.persist(tag);
		entityManager.getTransaction().commit();
		entityManager.close();

		entityManager = emf.createEntityManager();
		entityManager.getTransaction().begin();
		List<Tag> result = entityManager.createQuery("SELECT t FROM Tag t", Tag.class).getResultList();
		Assert.assertEquals(1, result.size());
		Assert.assertEquals("Our very first tag!", result.get(0).getName());
		Assert.assertEquals(tag.getId(), result.get(0).getId());
		entityManager.getTransaction().commit();
		entityManager.close();
	}
}
