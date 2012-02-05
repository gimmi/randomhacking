package com.github.gimmi.spikeeclipselinkjpa;

import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
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
	private static EntityManagerFactory entityManagerFactory;

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

		map.put("eclipselink.ddl-generation", "create-tables");

		entityManagerFactory = Persistence.createEntityManagerFactory("com.github.gimmi.spikeeclipselinkjpa", map);
	}

	@AfterClass
	public static void afterClass() {
		entityManagerFactory.close();
	}

	@Test
	public void testBasicUsage() {
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Event event = new Event();
		event.setTitle("Our very first event!");
		event.setDate(new Date());
		entityManager.persist(event);
		entityManager.getTransaction().commit();
		entityManager.close();

		entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Event> result = entityManager.createQuery("SELECT e FROM Event e", Event.class).getResultList();
		assertEquals(1, result.size());
		assertEquals("Our very first event!", result.get(0).getTitle());
		assertEquals(event.getId(), result.get(0).getId());
		entityManager.getTransaction().commit();
		entityManager.close();
	}

	@Test
	public void version_field() {
		Event event = new Event();
		assertThat(event.getVersion(), equalTo(0));

		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		entityManager.persist(event);
		assertEquals(0, event.getVersion());
		entityManager.getTransaction().commit();
		entityManager.close();

		assertThat(event.getVersion(), equalTo(1));
	}
}
