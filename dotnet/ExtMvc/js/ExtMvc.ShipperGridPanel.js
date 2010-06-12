/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace("ExtMvc");

ExtMvc.ShipperGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'ShipperId', header: 'ShipperId', xtype: 'numbercolumn' }, { dataIndex: 'CompanyName', header: 'CompanyName', xtype: 'gridcolumn' }, { dataIndex: 'Phone', header: 'Phone', xtype: 'gridcolumn' }
			]
		});
		ExtMvc.ShipperGridPanel.superclass.initComponent.apply(this, arguments);
	}
});