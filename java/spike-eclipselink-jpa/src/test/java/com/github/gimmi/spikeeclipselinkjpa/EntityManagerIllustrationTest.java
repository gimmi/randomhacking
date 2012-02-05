package com.github.gimmi.spikeeclipselinkjpa;

import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import javax.persistence.Persistence;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static junit.framework.Assert.assertEquals;
import static junit.framework.Assert.assertNull;
import static org.hamcrest.Matchers.equalTo;
import static org.junit.Assert.assertThat;

public class EntityManagerIllustrationTest {
	private static EntityManagerFactory emf;

	@BeforeClass
	public static void beforeClass() {
		Map<String, String> map = new HashMap<String, String>();

		map.put("javax.persistence.jdbc.driver", "org.h2.Driver");
		map.put("javax.persistence.jdbc.url", "jdbc:h2:mem:db1;DB_CLOSE_DELAY=-1;MVCC=TRUE");
		map.put("javax.persistence.jdbc.user", "sa");
		map.put("javax.persistence.jdbc.password", "");

		// See http://wiki.eclipse.org/EclipseLink/Examples/JPA/Logging
		map.put("eclipselink.logging.logger", "DefaultLogger");
		map.put("eclipselink.logging.level", "FINEST");

		// See http://wiki.eclipse.org/EclipseLink/Examples/JPA/DDL
		map.put("eclipselink.ddl-generation", "create-tables");

		emf = Persistence.createEntityManagerFactory("com.github.gimmi.spikeeclipselinkjpa", map);
	}

	@AfterClass
	public static void afterClass() {
		emf.close();
	}

	@Test
	public void testBasicUsage() {
		EntityManager entityManager = emf.createEntityManager();
		entityManager.getTransaction().begin();
		Task task = new Task();
		task.setTitle("Our very first task!");
		task.setDate(new Date());
		entityManager.persist(task);
		entityManager.getTransaction().commit();
		entityManager.close();

		entityManager = emf.createEntityManager();
		entityManager.getTransaction().begin();
		List<Task> result = entityManager.createQuery("SELECT e FROM Task e", Task.class).getResultList();
		assertEquals(1, result.size());
		assertEquals("Our very first task!", result.get(0).getTitle());
		assertEquals(task.getId(), result.get(0).getId());
		entityManager.getTransaction().commit();
		entityManager.close();
	}

	@Test
	public void version_field() {
		Task task = new Task();
		assertThat(task.getVersion(), equalTo(0));

		EntityManager entityManager = emf.createEntityManager();
		entityManager.getTransaction().begin();
		entityManager.persist(task);
		assertEquals(0, task.getVersion());
		entityManager.getTransaction().commit();
		entityManager.close();

		assertThat(task.getVersion(), equalTo(1));
	}

	@Test
	public void one_to_many() {
		Task task = new Task();
		Comment comment = new Comment();
		task.addComment(comment);

		EntityManager em = emf.createEntityManager();
		EntityTransaction tx = em.getTransaction();
		tx.begin();
		em.persist(task);
		tx.commit();
		em.close();

		em = emf.createEntityManager();
		task = em.find(Task.class, task.getId());
		assertThat(task.getComments().size(), equalTo(1));
		assertThat(task.getComments().iterator().next().getId(), equalTo(comment.getId()));
		em.close();
	}
}
