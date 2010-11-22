package com.github.gimmi.spikemavengwt.client;

import com.extjs.gxt.ui.client.widget.MessageBox;
import com.google.gwt.core.client.EntryPoint;

public class Spikemavengwt implements EntryPoint {
	@Override
	public void onModuleLoad() {
		MessageBox.info("Message", "Hello World!!", null);
	}
}
