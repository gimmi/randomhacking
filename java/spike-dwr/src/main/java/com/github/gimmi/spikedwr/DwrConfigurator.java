package com.github.gimmi.spikedwr;

import org.directwebremoting.fluent.FluentConfigurator;

public class DwrConfigurator extends FluentConfigurator {
	@Override
	public void configure() {
		withConverterType("dbrow", DbRowConverter.class.getName());
		withConverter("dbrow", DbRow.class.getName());
		withConverter("bean", LookupValue.class.getName());
		withCreator("new", RemoteMethods.class.getSimpleName())
				.addParam("class", RemoteMethods.class.getName());
	}
}
