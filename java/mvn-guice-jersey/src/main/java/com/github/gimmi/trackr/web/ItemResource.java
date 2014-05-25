package com.github.gimmi.trackr.web;

import com.github.gimmi.trackr.domain.Item;
import com.github.gimmi.trackr.domain.ItemRepository;

import javax.inject.Inject;
import javax.ws.rs.*;
import javax.ws.rs.core.*;
import java.util.List;

@Path("/items")
@Consumes("application/json")
@Produces("application/json")
public class ItemResource {
    @Inject
    ItemRepository itemRepository;

    @GET
    @Path("{id}")
    public Item get(@PathParam("id") String id) {
        Item item = itemRepository.get(id);
        if (item == null) {
            throw new WebApplicationException(Response.Status.NOT_FOUND);
        }
        return item;
    }

    @GET
    public List<Item> get() {
        return itemRepository.find();
    }

    @POST
    public Response post(Item item, @Context UriInfo ui) {
//		item = entityManager.merge(item);
        return Response.created(ui.getAbsolutePathBuilder().path(item.getId()).build()).build();
    }
}
