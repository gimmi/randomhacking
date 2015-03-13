package com.github.gimmi.jarweb;

import javax.inject.Inject;
import javax.json.Json;
import javax.json.JsonObject;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

@Path("myresource")
@Consumes(MediaType.APPLICATION_JSON)
@Produces(MediaType.APPLICATION_JSON)
public class MyResource {

    @Inject
    MyObject myObject;

    @GET
    public JsonObject getIt() {
        return Json.createObjectBuilder()
            .add("p1", myObject.getMessage())
            .add("p2", 42)
            .add("p3", true)
            .addNull("p4")
            .build();
    }
}
