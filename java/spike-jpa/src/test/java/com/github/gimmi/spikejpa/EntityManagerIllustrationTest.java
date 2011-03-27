package com.github.gimmi.spikejpa;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EntityManagerIllustrationTest {
	private EntityManagerFactory entityManagerFactory;

	@Before
	public void before() {
		Map<String, String> props = new HashMap<String, String>();
		entityManagerFactory = Persistence.createEntityManagerFactory("spikejpa", props);
	}

	@After
	public void after() {
		entityManagerFactory.close();
	}

	@Test
	public void testBasicUsage() {
		// create a couple of events...
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		entityManager.persist(new Event("Our very first event!", new Date()));
		entityManager.persist(new Event("A follow up event", new Date()));
		entityManager.getTransaction().commit();
		entityManager.close();

		// now lets pull events from the database and list them
		entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Event> result = entityManager.createQuery("from Event", Event.class).getResultList();
		for (Event event : result) {
			System.out.println("Event (" + event.getDate() + ") : " + event.getTitle());
		}
		entityManager.getTransaction().commit();
		entityManager.close();
	}
}
