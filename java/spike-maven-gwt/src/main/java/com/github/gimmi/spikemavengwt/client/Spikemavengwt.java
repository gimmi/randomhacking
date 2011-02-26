package com.github.gimmi.spikemavengwt.client;

import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.core.client.GWT;
import com.google.gwt.dom.client.Style.Unit;
import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.Window;
import com.google.gwt.user.client.ui.FlowPanel;
import com.google.gwt.user.client.ui.MenuBar;
import com.google.gwt.user.client.ui.RootLayoutPanel;

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

		MenuBar fooMenu = new MenuBar(true);
		fooMenu.addItem("the", cmd);
		fooMenu.addItem("foo", cmd);
		fooMenu.addItem("menu", cmd);

		MenuBar barMenu = new MenuBar(true);
		barMenu.addItem("the", cmd);
		barMenu.addItem("bar", cmd);
		barMenu.addItem("menu", cmd);

		MenuBar bazMenu = new MenuBar(true);
		bazMenu.addItem("the", cmd);
		bazMenu.addItem("baz", cmd);
		bazMenu.addItem("menu", cmd);

		MenuBar menu = new MenuBar();
		menu.addItem("foo", fooMenu);
		menu.addItem("bar", barMenu);
		menu.addItem("baz", bazMenu);

		FlowPanel flowPanel = new FlowPanel();
		rootLayoutPanel.add(flowPanel);
		rootLayoutPanel.setWidgetLeftWidth(flowPanel, 0.0, Unit.EM, 100.0, Unit.PCT);
		rootLayoutPanel.setWidgetTopHeight(flowPanel, 0.0, Unit.EM, 100.0, Unit.PCT);

		flowPanel.add(menu);
	}
}
