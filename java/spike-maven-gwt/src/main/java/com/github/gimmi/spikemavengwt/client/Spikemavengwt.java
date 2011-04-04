package com.github.gimmi.spikemavengwt.client;

import java.util.Arrays;
import java.util.List;

import com.google.gwt.cell.client.TextCell;
import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.core.client.GWT;
import com.google.gwt.dom.client.Style.Unit;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.user.cellview.client.CellList;
import com.google.gwt.user.cellview.client.CellTable;
import com.google.gwt.user.cellview.client.SimplePager;
import com.google.gwt.user.cellview.client.TextColumn;
import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.Timer;
import com.google.gwt.user.client.Window;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.DialogBox;
import com.google.gwt.user.client.ui.LayoutPanel;
import com.google.gwt.user.client.ui.RootLayoutPanel;
import com.google.gwt.user.client.ui.ScrollPanel;
import com.google.gwt.user.client.ui.TabLayoutPanel;
import com.google.gwt.view.client.AsyncDataProvider;
import com.google.gwt.view.client.HasData;
import com.google.gwt.view.client.Range;

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

		TabLayoutPanel tabLayoutPanel = new TabLayoutPanel(1.5, Unit.EM);

		tabLayoutPanel.add(buildListTab(), "List", false);
		tabLayoutPanel.add(buildTableTab(), "Table", false);
		tabLayoutPanel.add(buildAsyncListTab(), "Async List", false);
		tabLayoutPanel.add(buildDialogsTab(), "Dialogs", false);

		RootLayoutPanel rootLayoutPanel = RootLayoutPanel.get();
		rootLayoutPanel.add(tabLayoutPanel);
		rootLayoutPanel.setWidgetLeftWidth(tabLayoutPanel, 0.0, Unit.PX, 100.0, Unit.PCT);
		rootLayoutPanel.setWidgetTopHeight(tabLayoutPanel, 0.0, Unit.PX, 100.0, Unit.PCT);
	}

	private LayoutPanel buildListTab() {
		TextCell textCell = new TextCell();
		CellList<String> cellList = new CellList<String>(textCell);

		cellList.setRowCount(7, true);
		cellList.setRowData(0, Arrays.asList("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"));

		LayoutPanel layoutPanel = new LayoutPanel();
		layoutPanel.add(cellList);
		layoutPanel.setWidgetLeftWidth(cellList, 10.0, Unit.EM, 20.0, Unit.EM);
		layoutPanel.setWidgetTopHeight(cellList, 10.0, Unit.EM, 20.0, Unit.EM);
		return layoutPanel;
	}

	private LayoutPanel buildDialogsTab() {

		Button button = new Button("Show dialog");
		LayoutPanel layoutPanel = new LayoutPanel();
		layoutPanel.add(button);
		layoutPanel.setWidgetLeftWidth(button, 10.0, Unit.EM, 20.0, Unit.EM);
		layoutPanel.setWidgetTopHeight(button, 10.0, Unit.EM, 20.0, Unit.EM);

		button.addClickHandler(new ClickHandler() {
			@Override
			public void onClick(ClickEvent sender) {
				DialogBox dialogBox = new DialogBox();
				dialogBox.setText("Dialog window");
				Button btnNewButton = new Button("New button");
				btnNewButton.setSize("100px", "100px");
				dialogBox.setWidget(btnNewButton);
				dialogBox.center();
				dialogBox.show();
			}
		});

		return layoutPanel;
	}

	private LayoutPanel buildAsyncListTab() {
		TextCell textCell = new TextCell();
		final CellList<String> cellList = new CellList<String>(textCell);

		cellList.setVisibleRange(1, 3);

		AsyncDataProvider<String> dataProvider = new AsyncDataProvider<String>() {
			final List<String> data = Arrays.asList("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");

			@Override
			protected void onRangeChanged(final HasData<String> display) {
				new Timer() {
					@Override
					public void run() {
						// On the server...
						Range range = display.getVisibleRange();
						List<String> values = data.subList(range.getStart(), range.getStart() + range.getLength());
						// On the callback...
						display.setRowCount(data.size());
						display.setRowData(range.getStart(), values);
					}
				}.schedule(2000);
			}
		};
		dataProvider.addDataDisplay(cellList);

		LayoutPanel layoutPanel = new LayoutPanel();
		layoutPanel.add(cellList);
		layoutPanel.setWidgetLeftWidth(cellList, 10.0, Unit.EM, 20.0, Unit.EM);
		layoutPanel.setWidgetTopHeight(cellList, 10.0, Unit.EM, 20.0, Unit.EM);
		return layoutPanel;
	}

	private LayoutPanel buildTableTab() {
		CellTable<Dto> table = new CellTable<Dto>();
		table.addColumn(new TextColumn<Dto>() {
			@Override
			public String getValue(Dto contact) {
				return contact.name;
			}
		}, "Name");
		table.addColumn(new TextColumn<Dto>() {
			@Override
			public String getValue(Dto contact) {
				return contact.address;
			}
		}, "Address");
		table.setVisibleRange(0, 10);

		new AsyncDataProvider<Dto>() {
			@Override
			protected void onRangeChanged(final HasData<Dto> display) {
				final Range range = display.getVisibleRange();
				greetingService.getDtos(range.getStart(), range.getLength(), new AsyncCallback<PaginatedResults<Dto>>() {
					@Override
					public void onFailure(Throwable caught) {
						throw new RuntimeException(caught);
					}

					@Override
					public void onSuccess(PaginatedResults<Dto> result) {
						display.setRowCount(result.totalRows);
						display.setRowData(range.getStart(), result.results);
					}
				});
			}
		}.addDataDisplay(table);

		ScrollPanel scrollPanel = new ScrollPanel();
		scrollPanel.add(table);

		LayoutPanel layoutPanel = new LayoutPanel();
		layoutPanel.add(scrollPanel);
		layoutPanel.setWidgetLeftRight(scrollPanel, 10.0, Unit.EM, 10.0, Unit.EM);
		layoutPanel.setWidgetTopHeight(scrollPanel, 10.0, Unit.EM, 10.0, Unit.EM);

		SimplePager pager = new SimplePager();
		pager.setDisplay(table);
		layoutPanel.add(pager);
		layoutPanel.setWidgetLeftRight(pager, 10.0, Unit.EM, 10.0, Unit.EM);
		layoutPanel.setWidgetTopHeight(pager, 20.0, Unit.EM, 10.0, Unit.EM);
		return layoutPanel;
	}
}
