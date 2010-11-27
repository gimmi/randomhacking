package com.github.gimmi.spikemavengwt.client;

import com.extjs.gxt.ui.client.widget.ContentPanel;
import com.extjs.gxt.ui.client.widget.MessageBox;
import com.extjs.gxt.ui.client.widget.Viewport;
import com.extjs.gxt.ui.client.widget.button.Button;
import com.extjs.gxt.ui.client.widget.button.ToolButton;
import com.extjs.gxt.ui.client.widget.layout.FitLayout;
import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.core.client.GWT;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.RootPanel;

public class Spikemavengwt implements EntryPoint {
	private final GreetingServiceAsync greetingService = GWT.create(GreetingService.class);

	@Override
	public void onModuleLoad() {
		ContentPanel cp = new ContentPanel();
		cp.setHeading("Folder Contents");
		cp.setCollapsible(true);
		cp.setFrame(true);
		cp.setBodyStyle("backgroundColor: white;");
		cp.getHeader().addTool(new ToolButton("x-tool-gear"));
		cp.getHeader().addTool(new ToolButton("x-tool-close"));
		cp.addText("Ciao");
		cp.addButton(new Button("Ok"));
		cp.setIconStyle("tree-folder-open");

		Viewport viewport = new Viewport();
		viewport.setLayout(new FitLayout());
		viewport.add(cp);
		RootPanel.get().add(viewport);

		greetingService.greetServer("Gimmi", new AsyncCallback<String>() {
			public void onFailure(Throwable caught) {
				MessageBox.alert("Error", caught.getMessage(), null);
			}

			public void onSuccess(String result) {
				MessageBox.info("Success", result, null);
			}
		});
	}
}
