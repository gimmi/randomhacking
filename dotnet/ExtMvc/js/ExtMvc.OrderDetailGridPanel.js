/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace("ExtMvc");

ExtMvc.OrderDetailGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'OrderId', header: 'OrderId', xtype: 'numbercolumn' }, { dataIndex: 'ProductId', header: 'ProductId', xtype: 'numbercolumn' }, { dataIndex: 'UnitPrice', header: 'UnitPrice', xtype: 'numbercolumn' }, { dataIndex: 'Quantity', header: 'Quantity', xtype: 'numbercolumn' }, { dataIndex: 'Discount', header: 'Discount', xtype: 'numbercolumn' }
			]
		});
		ExtMvc.OrderDetailGridPanel.superclass.initComponent.apply(this, arguments);
	}
});