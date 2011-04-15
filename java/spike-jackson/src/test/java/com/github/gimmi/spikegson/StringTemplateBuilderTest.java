package com.github.gimmi.spikegson;

import org.junit.Test;

public class StringTemplateBuilderTest {
	private static class CustomType {
		public String value = "a value";
	}

	@Test
	public void should_serialize_custom_type_in_map_with_type_token() {
//		Map<String, Object> obj = new HashMap<String, Object>();
//		obj.put("k1", new CustomType());
//		obj.put("k2", 1);
//		obj.put("k3", "a string");
//
//		Type type = new TypeToken<Map<String, Object>>() {
//		}.getType();
//
//		String actual = new Gson().toJson(obj, type).replace('"', '\'');
//
//		assertEquals("{'k3':'a string','k1':{},'k2':1}", actual);
//
//		actual = new GsonBuilder().registerTypeAdapter(Object.class, new JsonSerializer<Object>() {
//			@Override
//			public JsonElement serialize(Object src, Type typeOfSrc, JsonSerializationContext context) {
//				return context.serialize(src);
//			}
//		}).create().toJson(obj, type).replace('"', '\'');
//
//		assertEquals("{'k3':'a string','k1':{'value':'a value'},'k2':1}", actual);
	}
}
