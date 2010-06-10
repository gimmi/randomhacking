/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.SupplierNormalSearchContainer = Ext.extend(Ext.Container, {
	layout: 'border',
	initComponent: function () {
		var store = new Ext.data.Store({
			autoDestroy: true,
			proxy: new Rpc.JsonPostHttpProxy({
				url: '/Supplier/SearchNormal'
			}),
			remoteSort: true,
			reader: new ExtMvc.SupplierJsonReader()
		});
		this.gridPanel = new ExtMvc.SupplierGridPanel({
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
				{ name: 'supplierId', xtype: 'numberfield', fieldLabel: 'supplierId' },
				{ name: 'companyName', xtype: 'textfield', fieldLabel: 'companyName' },
				{ name: 'contactName', xtype: 'textfield', fieldLabel: 'contactName' },
				{ name: 'contactTitle', xtype: 'textfield', fieldLabel: 'contactTitle' },
				{ name: 'address', xtype: 'textfield', fieldLabel: 'address' },
				{ name: 'city', xtype: 'textfield', fieldLabel: 'city' },
				{ name: 'region', xtype: 'textfield', fieldLabel: 'region' },
				{ name: 'postalCode', xtype: 'textfield', fieldLabel: 'postalCode' },
				{ name: 'country', xtype: 'textfield', fieldLabel: 'country' },
				{ name: 'phone', xtype: 'textfield', fieldLabel: 'phone' },
				{ name: 'fax', xtype: 'textfield', fieldLabel: 'fax' },
				{ name: 'homePage', xtype: 'textfield', fieldLabel: 'homePage' }
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

		ExtMvc.SupplierNormalSearchContainer.superclass.initComponent.call(this);
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