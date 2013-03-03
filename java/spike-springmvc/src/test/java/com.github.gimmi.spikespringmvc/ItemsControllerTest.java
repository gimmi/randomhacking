package com.github.gimmi.spikespringmvc;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.context.WebApplicationContext;

@RunWith(SpringJUnit4ClassRunner.class)
@WebAppConfiguration
@ContextConfiguration("servlet-context.xml")
public class ItemsControllerTest {

	@Autowired
	private WebApplicationContext wac;

	private MockMvc mockMvc;

	@Before
	public void setup() {
		this.mockMvc = webAppContextSetup(this.wac).build();
	}

	@Test
	public void getFoo() throws Exception {
		this.mockMvc.perform(get("/foo").accept("application/json"))
				.andExpect(status().isOk())
				.andExpect(content().mimeType("application/json"))
				.andExpect(jsonPath("$.name").value("Lee"));
	}
}
