/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ProductNormalSearchContainer = Ext.extend(Ext.Container, {
	layout: 'border',
	initComponent: function () {
		var store = new Ext.data.Store({
			autoDestroy: true,
			proxy: new Rpc.JsonPostHttpProxy({
				url: '/Product/SearchNormal'
			}),
			remoteSort: true,
			reader: new ExtMvc.ProductJsonReader()
		});
		this.gridPanel = new ExtMvc.ProductGridPanel({
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
			items: [
				{ name: 'productId', xtype: 'numberfield', fieldLabel: 'productId' },
				{ name: 'productName', xtype: 'textfield', fieldLabel: 'productName' },
				{ name: 'discontinued', xtype: 'checkbox', fieldLabel: 'discontinued' },
				{ name: 'category', xtype: 'ExtMvc.CategoryField', fieldLabel: 'category' },
				{ name: 'supplier', xtype: 'ExtMvc.SupplierField', fieldLabel: 'supplier' }
			],
			buttonAlign: 'left', 
			buttons: [{
				xtype: 'button',
				text: 'Search',
				handler: this.searchClick,
				scope: this
			}]
		});

		this.items = [this.searchFormPanel, this.gridPanel];

		this.addEvents('itemselected');

		ExtMvc.ProductNormalSearchContainer.superclass.initComponent.call(this);
	},

	gridPanel_rowDblClick: function (grid, rowIndex, event) {
		var item = grid.getStore().getAt(rowIndex).data;
		this.fireEvent('itemselected', this, item);
	},

	getSelectedItem: function () {
		var sm = this.gridPanel.getSelectionModel();
		return sm.getSelected().data;
	},

	searchClick: function (b, e) {
		var params = this.searchFormPanel.getForm().getFieldValues();
		Ext.apply(this.gridPanel.getStore().baseParams, params);
		this.gridPanel.getStore().load({
			params: {
				start: 0,
				limit: this.gridPanel.getBottomToolbar().pageSize
			}
		});
	}
});