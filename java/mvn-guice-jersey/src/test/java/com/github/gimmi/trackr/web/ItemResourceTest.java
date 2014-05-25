package com.github.gimmi.trackr.web;

import com.github.gimmi.trackr.TestDbHelpers;

import com.sun.jersey.api.client.Client;
import com.sun.jersey.api.client.ClientResponse;
import com.sun.jersey.api.client.config.ClientConfig;
import com.sun.jersey.api.client.config.DefaultClientConfig;
import org.codehaus.jackson.jaxrs.JacksonJsonProvider;
import org.codehaus.jackson.node.ArrayNode;
import org.codehaus.jackson.node.ObjectNode;
import org.eclipse.jetty.server.Server;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import java.util.List;
import java.util.Map;

import static org.hamcrest.CoreMatchers.equalTo;
import static org.junit.Assert.assertThat;
import static com.github.gimmi.trackr.TestServerHelpers.*;

public class ItemResourceTest {
	private Server server;
	private Client client;

	@Before
	public void before() throws Exception {
		TestDbHelpers.rebuildDatabase();

		server = buildWebServer();
		server.start();

		ClientConfig clientConfig = new DefaultClientConfig();
		clientConfig.getClasses().add(JacksonJsonProvider.class);
		client = Client.create(clientConfig);
	}

	@After
	public void after() throws Exception {
		server.stop();
		server.join();
	}

	@Test
	public void should_get_all_items() {
		TestDbHelpers.execSql("INSERT INTO ITEMS SET ID = 'id1', TITLE = 'item 1 title', VERSION = 1");

		ClientResponse resp = client.resource("http://localhost:8080/api/items")
				.accept("application/json")
				.get(ClientResponse.class);

		assertThat(resp.getStatus(), equalTo(200));
		ArrayNode items = resp.getEntity(ArrayNode.class);
		assertThat(items.size(), equalTo(1));
		assertThat(items.get(0).findPath("id").getTextValue(), equalTo("id1"));
	}

	@Test
	public void should_create_item() {
		ClientResponse resp = client.resource("http://localhost:8080/api/items")
				.accept("application/json")
				.type("application/json")
				.post(ClientResponse.class, "{ title: 'item title', tags: [ 'tag1' ] }");

		assertThat(resp.getStatus(), equalTo(201));

		List<Map<String, Object>> rows = TestDbHelpers.query("SELECT * FROM ITEMS");
		assertThat(rows.size(), equalTo(1));
		assertThat(rows.get(0).get("TITLE").toString(), equalTo("item title"));

		String itemLocation = resp.getHeaders().getFirst("Location");
		assertThat(itemLocation, equalTo("http://localhost:8080/api/items/" + rows.get(0).get("ID").toString()));

		rows = TestDbHelpers.query("SELECT * FROM TAGS");
		assertThat(rows.size(), equalTo(1));
		assertThat(rows.get(0).get("TAG").toString(), equalTo("tag1"));
	}

	@Test
	public void should_get_item_by_id() {
		TestDbHelpers.execSql("INSERT INTO ITEMS SET ID = 'id1', TITLE = 'item 1 title', VERSION = 1");

		ClientResponse resp = client.resource("http://localhost:8080/api/items/id1")
				.accept("application/json")
				.get(ClientResponse.class);

		assertThat(resp.getStatus(), equalTo(200));
		ObjectNode item = resp.getEntity(ObjectNode.class);
		assertThat(item.findPath("id").getTextValue(), equalTo("id1"));
	}
}
