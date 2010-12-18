package com.github.gimmi.spikedwr;

import java.util.Date;

import org.directwebremoting.fluent.FluentConfigurator;

public class DwrConfigurator extends FluentConfigurator {
	@Override
	public void configure() {
		withCreator("new", "JDate")
				.addParam("class", Date.class.getName());
	}
}
