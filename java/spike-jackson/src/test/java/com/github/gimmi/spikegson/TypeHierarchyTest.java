package com.github.gimmi.spikegson;

import org.codehaus.jackson.JsonGenerationException;
import org.codehaus.jackson.map.ObjectMapper;
import org.junit.Before;
import org.junit.Test;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import static junit.framework.Assert.assertEquals;

public class TypeHierarchyTest {
	public static abstract class BaseClass {
		public String baseField = "base field value";
		public List<BaseClass> collection = new ArrayList<BaseClass>();
	}

	public static class Class1 extends BaseClass {
		public String class1Field = "class 1 field value";
	}

	public static class Class2 extends BaseClass {
		public String class2Field = "class 2 field value";
	}

	BaseClass class1Instance;

	@Before
	public void before() {
		class1Instance = new Class1();
		class1Instance.collection.add(new Class2());
	}

	@Test
	public void should_just_consider_BaseClass_fields_without_typeadapter() throws IOException {
		ObjectMapper mapper = new ObjectMapper();
		String json = mapper.writeValueAsString(class1Instance).replace('"', '\'');

		assertEquals("{'baseField':'base field value','collection':[{'baseField':'base field value','collection':[],'class2Field':'class 2 field value'}],'class1Field':'class 1 field value'}", json);
	}
//
//	@Test
//	public void should_consider_all_fields_with_one_typeadapter_per_class() {
//		GsonBuilder builder = new GsonBuilder();
//		builder.registerTypeAdapter(Class1.class, new JsonSerializer<Class1>() {
//			@Override
//			public JsonElement serialize(Class1 src, Type typeOfSrc, JsonSerializationContext context) {
//				return context.serialize(src);
//			}
//		});
//		builder.registerTypeAdapter(Class2.class, new JsonSerializer<Class2>() {
//			@Override
//			public JsonElement serialize(Class2 src, Type typeOfSrc, JsonSerializationContext context) {
//				return context.serialize(src);
//			}
//		});
//		builder.registerTypeAdapter(BaseClass.class, new JsonSerializer<BaseClass>() {
//			@Override
//			public JsonElement serialize(BaseClass src, Type typeOfSrc, JsonSerializationContext context) {
//				// See http://groups.google.com/group/google-gson/browse_thread/thread/f7e827b1d2bf63e8/f1402131daa152f4
//				return context.serialize(src);
//			}
//		});
//
//		String json = builder.create().toJson(class1Instance).replace('"', '\'');
//
//		assertEquals("{'class1Field':'class 1 field value','baseField':'base field value','collection':[{'class2Field':'class 2 field value','baseField':'base field value','collection':[]}]}", json);
//	}
}
