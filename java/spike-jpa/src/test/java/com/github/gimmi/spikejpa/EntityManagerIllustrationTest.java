package com.github.gimmi.spikejpa;

import java.util.Date;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

import com.github.gimmi.spikejpa.Event;
import junit.framework.TestCase;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;

public class EntityManagerIllustrationTest {
	private EntityManagerFactory entityManagerFactory;

	@Before
	public void before() {
		entityManagerFactory = Persistence.createEntityManagerFactory("spikejpa");
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
        List<Event> result = entityManager.createQuery( "from Event", Event.class ).getResultList();
		for ( Event event : result ) {
			System.out.println( "Event (" + event.getDate() + ") : " + event.getTitle() );
		}
        entityManager.getTransaction().commit();
        entityManager.close();
	}
}
