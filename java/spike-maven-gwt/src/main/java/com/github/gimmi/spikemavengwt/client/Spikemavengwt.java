package com.github.gimmi.spikemavengwt.client;

import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.core.client.GWT;
import com.google.gwt.dom.client.Style.Unit;
import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.Window;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.HTML;
import com.google.gwt.user.client.ui.LayoutPanel;
import com.google.gwt.user.client.ui.RootLayoutPanel;
import com.google.gwt.user.client.ui.TabLayoutPanel;

public class Spikemavengwt implements EntryPoint {
	private final GreetingServiceAsync greetingService = GWT.create(GreetingService.class);

	@Override
	public void onModuleLoad() {
		Command cmd = new Command() {
			@Override
			public void execute() {
				Window.alert("You selected a menu item!");
			}
		};
		RootLayoutPanel rootLayoutPanel = RootLayoutPanel.get();

		TabLayoutPanel tabLayoutPanel = new TabLayoutPanel(1.5, Unit.EM);

		LayoutPanel layoutPanel = new LayoutPanel();
		tabLayoutPanel.add(layoutPanel, "New Widget", false);

		Button btnNewButton = new Button("New button");
		layoutPanel.add(btnNewButton);
		layoutPanel.setWidgetLeftRight(btnNewButton, 10.0, Unit.EM, 10.0, Unit.EM);
		layoutPanel.setWidgetTopBottom(btnNewButton, 10.0, Unit.EM, 10.0, Unit.EM);
		rootLayoutPanel.add(tabLayoutPanel);

		HTML htmlCiao = new HTML("Ciao", true);
		tabLayoutPanel.add(htmlCiao, "New Widget", false);

		HTML htmlCiao_1 = new HTML("Ciao 2", true);
		tabLayoutPanel.add(htmlCiao_1, "New Widget", false);
	}
}
