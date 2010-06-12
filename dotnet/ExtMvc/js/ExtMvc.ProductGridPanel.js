/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace("ExtMvc");

ExtMvc.ProductGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'ProductId', header: 'ProductId', xtype: 'numbercolumn' }, { dataIndex: 'ProductName', header: 'ProductName', xtype: 'gridcolumn' }, { dataIndex: 'QuantityPerUnit', header: 'QuantityPerUnit', xtype: 'gridcolumn' }, { dataIndex: 'UnitPrice', header: 'UnitPrice', xtype: 'numbercolumn' }, { dataIndex: 'UnitsInStock', header: 'UnitsInStock', xtype: 'numbercolumn' }, { dataIndex: 'UnitsOnOrder', header: 'UnitsOnOrder', xtype: 'numbercolumn' }, { dataIndex: 'ReorderLevel', header: 'ReorderLevel', xtype: 'numbercolumn' }, { dataIndex: 'Discontinued', header: 'Discontinued', xtype: 'booleancolumn' }, { dataIndex: 'Category', header: 'Category', xtype: 'ExtMvc.CategoryColumn' }
			]
		});
		ExtMvc.ProductGridPanel.superclass.initComponent.apply(this, arguments);
	}
});