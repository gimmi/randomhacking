/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace("ExtMvc");

ExtMvc.OrderGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'OrderId', header: 'OrderId', xtype: 'numbercolumn' }, { dataIndex: 'OrderDate', header: 'OrderDate', xtype: 'datecolumn' }, { dataIndex: 'RequiredDate', header: 'RequiredDate', xtype: 'datecolumn' }, { dataIndex: 'ShippedDate', header: 'ShippedDate', xtype: 'datecolumn' }, { dataIndex: 'Freight', header: 'Freight', xtype: 'numbercolumn' }, { dataIndex: 'ShipName', header: 'ShipName', xtype: 'gridcolumn' }, { dataIndex: 'ShipAddress', header: 'ShipAddress', xtype: 'gridcolumn' }, { dataIndex: 'ShipCity', header: 'ShipCity', xtype: 'gridcolumn' }, { dataIndex: 'ShipRegion', header: 'ShipRegion', xtype: 'gridcolumn' }, { dataIndex: 'ShipPostalCode', header: 'ShipPostalCode', xtype: 'gridcolumn' }, { dataIndex: 'ShipCountry', header: 'ShipCountry', xtype: 'gridcolumn' }, { dataIndex: 'Customer', header: 'Customer', xtype: 'ExtMvc.Ns.CustomerColumn' }, { dataIndex: 'Employee', header: 'Employee', xtype: 'ExtMvc.Ns.EmployeeColumn' }
			]
		});
		ExtMvc.OrderGridPanel.superclass.initComponent.apply(this, arguments);
	}
});