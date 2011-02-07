package com.github.gimmi.spikestringtemplate;

import static org.hamcrest.Matchers.containsString;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertThat;
import static org.junit.Assert.fail;

import org.antlr.stringtemplate.StringTemplate;
import org.junit.Before;
import org.junit.Test;

public class StringTemplateBuilderTest {
	private StringTemplateBuilder target;

	@Before
	public void before() {
		target = new StringTemplateBuilder();
	}

	@Test
	public void should_process_template_loaded_from_classpath() {
		StringTemplate st = target.build("simpleTemplate");
		st.setAttribute("name", "World");
		assertEquals("Hello, World", st.toString());
	}

	@Test
	public void should_throw_exception_when_template_is_malformed() {
		try {
			target.build("malformedTemplate");
			fail();
		} catch (Exception e) {
			assertThat(e.getMessage(), containsString("problem parsing template"));
		}
	}

	@Test
	public void should_process_subtemplates() {
		StringTemplate st = target.build("parentTemplate");
		st.setAttribute("name", "World");
		assertEquals("Parent template (name = World), Child template (param = World, name = World)", st.toString());
	}

}
