package com.github.gimmi.spikemavengae.data;

import static org.junit.Assert.assertEquals;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import com.github.gimmi.spikemavengae.domain.Comment;
import com.github.gimmi.spikemavengae.domain.Ticket;
import com.google.appengine.api.datastore.DatastoreService;
import com.google.appengine.api.datastore.DatastoreServiceFactory;
import com.google.appengine.tools.development.testing.LocalDatastoreServiceTestConfig;
import com.google.appengine.tools.development.testing.LocalServiceTestHelper;

public class ObjectDatastoreTest {
	LocalServiceTestHelper helper = new LocalServiceTestHelper(new LocalDatastoreServiceTestConfig());
	DatastoreService ds;
	ObjectDatastore target;

	@Before
	public void setUp() {
		helper.setUp();
		ds = DatastoreServiceFactory.getDatastoreService();
		target = new ObjectDatastore(ds);
		target.registerConverter(Ticket.class, new TicketConverter());
		target.registerConverter(Comment.class, new CommentConverter());
	}

	@After
	public void tearDown() {
		helper.tearDown();
	}

	@Test
	public void doTest() {
		Ticket ticket = new Ticket();
		ticket.title = "a ticket";
		target.put(ticket);
		ticket = target.get(Ticket.class, ticket.id);

		assertEquals("a ticket", ticket.title);

		Comment comment = new Comment();
		comment.user = "gimmi";
		comment.text = "a comment";
		ticket.comments.add(comment);
		target.put(ticket);
		ticket = target.get(Ticket.class, ticket.id);

		assertEquals(1, ticket.comments.size());
		assertEquals("a comment", ticket.comments.get(0).text);

		ticket.comments.clear();
		target.put(ticket);
		ticket = target.get(Ticket.class, ticket.id);

		assertEquals(0, ticket.comments.size());
	}
}
