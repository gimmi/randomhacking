/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ShipperNormalSearchContainer = Ext.extend(Ext.Container, {
	layout: 'border',
	initComponent: function () {
		var store = new Ext.data.Store({
			proxy: new Rpc.JsonPostHttpProxy({
				url: '/Shipper/SearchNormal'
			}),
			remoteSort: true,
			reader: new ExtMvc.ShipperJsonReader()
		});
		this.gridPanel = new ExtMvc.ShipperGridPanel({
			region: 'center',
			store: store,
			bbar: new Ext.PagingToolbar({
				store: store,
				displayInfo: true,
				pageSize: 25,
				prependButtons: true,
				// TODO check http://www.extjs.com/forum/showthread.php?100775
				listeners: {
					beforechange: function (paging, params) {
						var lastParams = (paging.store.lastOptions || {}).params || {};
						Ext.applyIf(params, lastParams);
					}
				}
			}),
			listeners: {
				rowdblclick: {
					fn: this.gridPanel_rowDblClick,
					scope: this
				}
			}
		});

		this.searchFormPanel = new Ext.form.FormPanel({
			title: 'Search Filters',
			region: 'north',
			autoHeight: true,
			collapsible: true,
			titleCollapse: true,
			floatable: false,
			labelWidth: 100,
			border: false,
			padding: 10,
			items: [{ name: 'shipperId', xtype: 'textfield', fieldLabel: 'shipperId', anchor: '100%' }, { name: 'companyName', xtype: 'textfield', fieldLabel: 'companyName', anchor: '100%' }, { name: 'phone', xtype: 'textfield', fieldLabel: 'phone', anchor: '100%' }],
			buttons: [{
				xtype: 'button',
				text: 'Search',
				handler: this.searchClick,
				scope: this
			}]
		});

		this.items = [this.searchFormPanel, this.gridPanel];

		this.addEvents('itemselected');

		ExtMvc.ShipperNormalSearchContainer.superclass.initComponent.call(this);
	},

	gridPanel_rowDblClick: function (grid, rowIndex, event) {
		var item = grid.getStore().getAt(rowIndex).data;
		this.fireEvent('itemselected', this, item);
	},

	getSelectedItem: function () {
		var sm = this.gridPanel.getSelectionModel();
		return sm.getCount() === 1 ? sm.getSelected().data : null;
	},

	searchClick: function (b, e) {
		var args = this.searchFormPanel.getForm().getFieldValues();
		this.gridPanel.getStore().load({
			params: Ext.apply({
				start: 0,
				limit: this.gridPanel.getBottomToolbar().pageSize
			}, args)
		});
	}
});