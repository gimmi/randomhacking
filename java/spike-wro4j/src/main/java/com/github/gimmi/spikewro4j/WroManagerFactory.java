package com.github.gimmi.spikewro4j;

import java.io.IOException;
import java.io.InputStream;

import javax.servlet.ServletContext;

import ro.isdc.wro.manager.factory.BaseWroManagerFactory;
import ro.isdc.wro.model.factory.FallbackAwareXmlModelFactory;
import ro.isdc.wro.model.factory.WroModelFactory;

public class WroManagerFactory extends BaseWroManagerFactory {

	@Override
	protected WroModelFactory newModelFactory(ServletContext servletContext) {
		return new FallbackAwareXmlModelFactory() {
			@Override
			protected InputStream getConfigResourceAsStream() throws IOException {
				InputStream stream = getClass().getResourceAsStream("wro.xml");
				return stream;
			}
		};
	}

}
