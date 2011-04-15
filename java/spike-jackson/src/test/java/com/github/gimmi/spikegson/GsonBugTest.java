package com.github.gimmi.spikegson;

import com.google.common.collect.Lists;
import org.codehaus.jackson.map.ObjectMapper;
import org.junit.Test;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.assertEquals;

public class GsonBugTest {

	public static class AnotherClass {
		private String anotherClassField = "base field value";
		public Class1 class1Field;
		public List<Class1> listOfClass = new ArrayList<Class1>();
	}

	public static class Class1 {
		public String class1Field = "class 1 field value";
	}

	public static class Class2 extends Class1 {
		public String class2Field = "class 2 field value";
	}

	@Test
	public void should_serialize_only_Class1_fields() throws IOException {
		ObjectMapper mapper = new ObjectMapper();
		AnotherClass anotherClass = new AnotherClass();
		anotherClass.class1Field = new Class2();
		anotherClass.listOfClass.add(new Class2());
		String json = mapper.writeValueAsString(anotherClass).replace('"', '\'');

		assertEquals("{'class1Field':{'class1Field':'class 1 field value','class2Field':'class 2 field value'},'listOfClass':[{'class1Field':'class 1 field value','class2Field':'class 2 field value'}]}", json);
	}
}
