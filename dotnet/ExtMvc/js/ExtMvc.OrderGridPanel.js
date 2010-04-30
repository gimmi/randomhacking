﻿"use strict";

Ext.namespace("ExtMvc");

ExtMvc.OrderGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.store = new Ext.data.Store({
			proxy: new Rpc.JsonPostHttpProxy({
				url: '/Order/Find'
			}),
			remoteSort: true,
			reader: new Ext.data.JsonReader({
				root: 'items',
				idProperty: "OrderId",
				totalProperty: 'count',
				fields: ['OrderId', 'OrderDate', 'RequiredDate', 'ShippedDate', 'Freight', 'ShipName', 'ShipAddress', 'ShipCity', 'ShipRegion', 'ShipPostalCode', 'ShipCountry']
			})
		});

		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'OrderId', header: 'OrderId' },
				{ dataIndex: 'OrderDate', header: 'OrderDate' },
				{ dataIndex: 'RequiredDate', header: 'RequiredDate' },
				{ dataIndex: 'ShippedDate', header: 'ShippedDate' },
				{ dataIndex: 'Freight', header: 'Freight' },
				{ dataIndex: 'ShipName', header: 'ShipName' },
				{ dataIndex: 'ShipAddress', header: 'ShipAddress' },
				{ dataIndex: 'ShipCity', header: 'ShipCity' },
				{ dataIndex: 'ShipRegion', header: 'ShipRegion' },
				{ dataIndex: 'ShipPostalCode', header: 'ShipPostalCode' },
				{ dataIndex: 'ShipCountry', header: 'ShipCountry' }
			]
		});

		this.bbar = new Ext.PagingToolbar({
			store: this.store,
			displayInfo: true,
			pageSize: 25,
			prependButtons: true
		});

		ExtMvc.OrderGridPanel.superclass.initComponent.call(this);
	}
});