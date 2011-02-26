package com.github.gimmi.spikemavengwt.client;

import java.util.Arrays;
import java.util.List;

import com.google.gwt.cell.client.TextCell;
import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.core.client.GWT;
import com.google.gwt.dom.client.Style.Unit;
import com.google.gwt.user.cellview.client.CellList;
import com.google.gwt.user.cellview.client.CellTable;
import com.google.gwt.user.cellview.client.TextColumn;
import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.Timer;
import com.google.gwt.user.client.Window;
import com.google.gwt.user.client.ui.LayoutPanel;
import com.google.gwt.user.client.ui.RootLayoutPanel;
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

	private LayoutPanel buildAsyncListTab() {
		TextCell textCell = new TextCell();
		final CellList<String> cellList = new CellList<String>(textCell);

		final List<String> data = Arrays.asList("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
		cellList.setVisibleRange(1, 3);
		cellList.setRowCount(data.size()); // Should be asyncronously asked to the server

		AsyncDataProvider<String> dataProvider = new AsyncDataProvider<String>() {
			@Override
			protected void onRangeChanged(final HasData<String> display) {
				new Timer() {
					@Override
					public void run() {
						// On the server...
						Range range = display.getVisibleRange();
						List<String> values = data.subList(range.getStart(), range.getStart() + range.getLength());
						// On the callback...
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
		CellTable<Contact> table = new CellTable<Contact>();
		table.addColumn(new TextColumn<Contact>() {
			@Override
			public String getValue(Contact contact) {
				return contact.name;
			}
		}, "Name");
		table.addColumn(new TextColumn<Contact>() {
			@Override
			public String getValue(Contact contact) {
				return contact.address;
			}
		}, "Address");

		List<Contact> contacts = Arrays.asList(new Contact("John", "123 Fourth Road"), new Contact("Mary", "222 Lancer Lane"));
		table.setRowCount(contacts.size(), true);
		table.setRowData(0, contacts);

		LayoutPanel layoutPanel = new LayoutPanel();
		layoutPanel.add(table);
		layoutPanel.setWidgetLeftWidth(table, 10.0, Unit.EM, 20.0, Unit.EM);
		layoutPanel.setWidgetTopHeight(table, 10.0, Unit.EM, 20.0, Unit.EM);
		return layoutPanel;
	}

	private static class Contact {
		private final String address;
		private final String name;

		public Contact(String name, String address) {
			this.name = name;
			this.address = address;
		}
	}
}
