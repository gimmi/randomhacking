package com.github.gimmi.spikegson;

import com.google.gson.Gson;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.assertEquals;

public class GsonBugTest {

	public static class AnotherClass {
		private String anotherClassField = "base field value";
		public Class1 class1Field;
	}

	public static class Class1 {
		public String class1Field = "class 1 field value";
	}

	public static class Class2 extends Class1 {
		public String class2Field = "class 2 field value";
	}

	@Test
	public void should_serialize_only_Class1_fields() {

		String json = new Gson().toJson(new Class2(), Class1.class).replace('"', '\'');

		assertEquals("{'class1Field':'class 1 field value'}", json);
	}

	@Test
	public void should_serialize_Class1_and_Class2_fields() {
		String json = new Gson().toJson(new Class2(), Class2.class).replace('"', '\'');

		assertEquals("{'class2Field':'class 2 field value','class1Field':'class 1 field value'}", json);
	}

	@Test
	public void should_serialize_only_Class1_fields_when_encounter_a_Class1_reference_that_hold_a_Class2_instance() {
		AnotherClass anotherClass = new AnotherClass();
		anotherClass.class1Field = new Class2();
		String json = new Gson().toJson(anotherClass, AnotherClass.class).replace('"', '\'');

		assertEquals("{'anotherClassField':'base field value','class1Field':{'class1Field':'class 1 field value'}}", json);

	}

	public static class ClassWithCollection {
		private String anotherClassField = "base field value";
		public List<Class1> collection = new ArrayList<Class1>();
	}

	@Test
	public void should_serialize_only_Class1_fields_when_encounter_a_collection_of_Class1_reference_that_hold_a_Class2_instances() {
		ClassWithCollection anotherClass = new ClassWithCollection();
		anotherClass.collection.add(new Class2());
		String json = new Gson().toJson(anotherClass, ClassWithCollection.class).replace('"', '\'');

		assertEquals("{'anotherClassField':'base field value','collection':[{'class1Field':'class 1 field value'}]}", json);
	}
}
