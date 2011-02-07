package com.github.gimmi.spikestringtemplate;

import static org.junit.Assert.assertEquals;

import org.antlr.stringtemplate.StringTemplate;
import org.antlr.stringtemplate.StringTemplateGroup;
import org.junit.Test;

public class StringTemplateTest {
	@Test
	public void should_process_template_constructed_from_string() {
		StringTemplate hello = new StringTemplate("Hello, $name$");
		hello.setAttribute("name", "World");
		assertEquals("Hello, World", hello.toString());
	}

	@Test
	public void should_process_template_loaded_from_classpath() {
		StringTemplateGroup group = new StringTemplateGroupBuilder().build();
		StringTemplate st = group.getInstanceOf("com/github/gimmi/spikestringtemplate/testTemplate");
		st.setAttribute("name", "World");
		assertEquals("Hello, World", st.toString());
	}

	@Test
	public void should_process_subtemplates() {
		StringTemplateGroup group = new StringTemplateGroupBuilder().build();
		StringTemplate st = group.getInstanceOf("com/github/gimmi/spikestringtemplate/parentTemplate");
		st.setAttribute("name", "World");
		assertEquals("Parent template (name = World), Child template (param = World, name = World)", st.toString());
	}
}
