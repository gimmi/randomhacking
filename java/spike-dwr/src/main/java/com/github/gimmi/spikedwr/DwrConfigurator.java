package com.github.gimmi.spikedwr;

import org.directwebremoting.fluent.FluentConfigurator;

public class DwrConfigurator extends FluentConfigurator {
	@Override
	public void configure() {
		withCreator("new", RemoteMethods.class.getSimpleName())
				.addParam("class", RemoteMethods.class.getName());
	}
}
