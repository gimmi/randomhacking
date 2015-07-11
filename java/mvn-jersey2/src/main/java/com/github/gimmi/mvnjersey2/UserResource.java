package com.github.gimmi.mvnjersey2;

import javax.inject.Inject;
import javax.json.Json;
import javax.json.JsonArray;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

@Path("users")
public class UserResource {

    @Inject
    MyRequestScopedService requestScopedService;

    @Inject
    MySingletonScopedService singletonScopedService;

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public JsonArray getJson() {
        return Json.createArrayBuilder()
            .add(Json.createObjectBuilder()
                .add("name", "Agamemnon")
                .add("age", 32))
            .add(Json.createObjectBuilder()
                .add("name", "Attila")
                .add("age", 68))
            .build();
    }

    @GET
    @Path("requestscopedservice")
    @Produces(MediaType.TEXT_PLAIN)
    public String getRequestScoped() {
        return requestScopedService.getString();
    }

    @GET
    @Path("singletonscopedservice")
    @Produces(MediaType.TEXT_PLAIN)
    public String getSingletonScoped() {
        return singletonScopedService.getString();
    }
}
