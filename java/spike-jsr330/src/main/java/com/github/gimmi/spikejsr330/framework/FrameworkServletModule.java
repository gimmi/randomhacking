package com.github.gimmi.spikejsr330.framework;

import com.google.inject.servlet.ServletModule;

public class FrameworkServletModule extends ServletModule {
	@Override
	protected void configureServlets() {
		serve("/TestServlet").with(TestServlet.class);
	}
}
