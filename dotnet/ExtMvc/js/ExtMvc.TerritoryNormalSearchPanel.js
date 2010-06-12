/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.TerritoryNormalSearchPanel = Ext.extend(Ext.Panel, {
	layout: 'border',
	border: false,
	initComponent: function () {
		var store = new Ext.data.Store({
			autoDestroy: true,
			proxy: new Rpc.JsonPostHttpProxy({
				url: '/Territory/SearchNormal'
			}),
			remoteSort: true,
			reader: new ExtMvc.TerritoryJsonReader()
		});
		this.gridPanel = new ExtMvc.TerritoryGridPanel({
			region: 'center',
			store: store,
			bbar: new Ext.PagingToolbar({
				store: store,
				displayInfo: true,
				pageSize: 25,
				prependButtons: true
			}),
			listeners: {
				rowdblclick: {
					fn: this.gridPanel_rowDblClick,
					scope: this
				}
			}
		});

		this.searchFormPanel = new ExtMvc.TerritoryNormalSearchFormPanel({
			title: 'Search Filters',
			region: 'north',
			autoHeight: true,
			collapsible: true,
			collapsed: true,
			titleCollapse: true,
			floatable: false
		});

		this.items = [this.searchFormPanel, this.gridPanel];

		this.tbar = [
			{ text: 'Search', handler: this.onSearchButtonClick, icon: '/images/zoom.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'New', handler: this.onNewButtonClick, icon: '/images/add.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'Edit', handler: this.onEditButtonClick, icon: '/images/pencil.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'Delete', handler: this.onDeleteButtonClick, icon: '/images/delete.png', cls: 'x-btn-text-icon', scope: this }
		];

		this.addEvents('itemselected');

		ExtMvc.TerritoryNormalSearchPanel.superclass.initComponent.apply(this, arguments);
	},

	gridPanel_rowDblClick: function (grid, rowIndex, event) {
		var item = grid.getStore().getAt(rowIndex).data;
		this.fireEvent('itemselected', this, item);
	},

	getSelectedItem: function () {
		var sm = this.gridPanel.getSelectionModel();
		return sm.getCount() > 0 ? sm.getSelected().data : null;
	},

	onSearchButtonClick: function (b, e) {
		var params = this.searchFormPanel.getForm().getFieldValues();
		Ext.apply(this.gridPanel.getStore().baseParams, params);
		this.gridPanel.getStore().load({
			params: {
				start: 0,
				limit: this.gridPanel.getBottomToolbar().pageSize
			}
		});
	},

	onNewButtonClick: function () {
		alert('onNewButtonClick');
	},

	onEditButtonClick: function () {
		alert('onEditButtonClick');
	},

	onDeleteButtonClick: function () {
		alert('onDeleteButtonClick');
	}
});