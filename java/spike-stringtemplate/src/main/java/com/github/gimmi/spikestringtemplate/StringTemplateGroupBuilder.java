package com.github.gimmi.spikestringtemplate;

import org.antlr.stringtemplate.StringTemplateErrorListener;
import org.antlr.stringtemplate.StringTemplateGroup;

public class StringTemplateGroupBuilder {
	public StringTemplateGroup build() {
		StringTemplateGroup group = new StringTemplateGroup("templates");
		group.setErrorListener(new ExceptionStringTemplateErrorListener());
		return group;
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
