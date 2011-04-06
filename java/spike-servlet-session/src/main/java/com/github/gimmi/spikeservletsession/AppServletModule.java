package com.github.gimmi.spikeservletsession;

import com.google.inject.servlet.ServletModule;

public class AppServletModule extends ServletModule {
	@Override
	protected void configureServlets() {
		serve("/TestServlet").with(TestServlet.class);
	}
}
