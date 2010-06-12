/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace("ExtMvc");

ExtMvc.CategoryGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'CategoryId', header: 'CategoryId', xtype: 'numbercolumn' }, { dataIndex: 'CategoryName', header: 'CategoryName', xtype: 'gridcolumn' }, { dataIndex: 'Description', header: 'Description', xtype: 'gridcolumn' }
			]
		});
		ExtMvc.CategoryGridPanel.superclass.initComponent.apply(this, arguments);
	}
});