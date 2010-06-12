/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace("ExtMvc");

ExtMvc.SupplierGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'SupplierId', header: 'SupplierId', xtype: 'numbercolumn' }, { dataIndex: 'CompanyName', header: 'CompanyName', xtype: 'gridcolumn' }, { dataIndex: 'ContactName', header: 'ContactName', xtype: 'gridcolumn' }, { dataIndex: 'ContactTitle', header: 'ContactTitle', xtype: 'gridcolumn' }, { dataIndex: 'Address', header: 'Address', xtype: 'gridcolumn' }, { dataIndex: 'City', header: 'City', xtype: 'gridcolumn' }, { dataIndex: 'Region', header: 'Region', xtype: 'gridcolumn' }, { dataIndex: 'PostalCode', header: 'PostalCode', xtype: 'gridcolumn' }, { dataIndex: 'Country', header: 'Country', xtype: 'gridcolumn' }, { dataIndex: 'Phone', header: 'Phone', xtype: 'gridcolumn' }, { dataIndex: 'Fax', header: 'Fax', xtype: 'gridcolumn' }, { dataIndex: 'HomePage', header: 'HomePage', xtype: 'gridcolumn' }
			]
		});
		ExtMvc.SupplierGridPanel.superclass.initComponent.apply(this, arguments);
	}
});