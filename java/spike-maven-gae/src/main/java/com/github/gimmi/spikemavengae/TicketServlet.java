package com.github.gimmi.spikemavengae;

import java.io.IOException;
import java.util.logging.Logger;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.github.gimmi.spikemavengae.data.CommentConverter;
import com.github.gimmi.spikemavengae.data.ObjectDatastore;
import com.github.gimmi.spikemavengae.data.TicketConverter;
import com.github.gimmi.spikemavengae.domain.Comment;
import com.github.gimmi.spikemavengae.domain.Ticket;
import com.google.appengine.api.datastore.DatastoreServiceFactory;

@SuppressWarnings("serial")
public class TicketServlet extends HttpServlet {
	Logger logger = Logger.getLogger(TicketServlet.class.getName());

	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		ObjectDatastore ds = new ObjectDatastore(DatastoreServiceFactory.getDatastoreService());
		ds.registerConverter(Ticket.class, new TicketConverter());
		ds.registerConverter(Comment.class, new CommentConverter());

		Ticket ticket = new Ticket();
		ticket.title = "a ticket";
		Comment comment = new Comment();
		comment.user = "gimmi";
		comment.text = "a comment";
		ticket.comments.add(comment);
		ds.put(ticket);

		logger.info("Added ticket " + ticket.id);
	}
}
