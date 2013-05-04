package com.github.gimmi.trackr.web;

import com.github.gimmi.trackr.domain.Item;
import com.google.inject.Inject;
import com.google.inject.persist.Transactional;

import javax.persistence.EntityManager;
import javax.persistence.TypedQuery;
import javax.ws.rs.*;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.UriInfo;
import java.util.List;

@Path("/items")
@Consumes("application/json")
@Produces("application/json")
public class ItemResource {
	@Inject
	EntityManager entityManager;

	@GET
	@Path("{id}")
	public Item get(@PathParam("id") String id) {
		Item item = entityManager.find(Item.class, id);
		if (item == null) {
			throw new WebApplicationException(Response.Status.NOT_FOUND);
		}
		return item;
	}

	@GET
	public List<Item> get() {
		return entityManager.createQuery("SELECT i FROM Item i", Item.class).getResultList();
	}

	@POST
	@Transactional
	public Response post(Item item, @Context UriInfo ui) {
		item = entityManager.merge(item);
		return Response.created(ui.getAbsolutePathBuilder().path(item.getId()).build()).build();
	}
}
