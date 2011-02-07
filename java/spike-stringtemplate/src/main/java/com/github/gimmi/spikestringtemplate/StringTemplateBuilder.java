package com.github.gimmi.spikestringtemplate;

import org.antlr.stringtemplate.StringTemplate;
import org.antlr.stringtemplate.StringTemplateErrorListener;
import org.antlr.stringtemplate.StringTemplateGroup;

public class StringTemplateBuilder {
	public StringTemplate build(String templateName) {
		StringTemplateGroup group = new StringTemplateGroup("templates");
		group.setErrorListener(new ExceptionStringTemplateErrorListener());
		String templatePrefix = getClass().getPackage().getName().replace('.', '/') + "/";
		return group.getInstanceOf(templatePrefix + templateName);
	}

	public static class ExceptionStringTemplateErrorListener implements StringTemplateErrorListener {
		@Override
		public void warning(String msg) {
			throw new RuntimeException(msg);
		}

		@Override
		public void error(String msg, Throwable e) {
			throw new RuntimeException(msg, e);
		}
	}
}
