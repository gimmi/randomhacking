package com.github.gimmi.mvnjersey2;

import java.util.Arrays;
import java.util.List;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

@Path("users")
public class UserResource {

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public List<User> getJson() {
        return Arrays.asList(new User("myself"), new User("other"));
    }

    @javax.xml.bind.annotation.XmlRootElement
    public static class User {

        public String name;

        public User() {
        }

        public User(String name) {
            this.name = name;
        }

    }

}
