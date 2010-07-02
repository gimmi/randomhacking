/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace("ExtMvc");

ExtMvc.AddressGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'Name', header: 'Name', xtype: 'gridcolumn' }, { dataIndex: 'AddressString', header: 'AddressString', xtype: 'gridcolumn' }, { dataIndex: 'City', header: 'City', xtype: 'gridcolumn' }, { dataIndex: 'Region', header: 'Region', xtype: 'gridcolumn' }, { dataIndex: 'PostalCode', header: 'PostalCode', xtype: 'gridcolumn' }, { dataIndex: 'Country', header: 'Country', xtype: 'gridcolumn' }
			]
		});
		ExtMvc.AddressGridPanel.superclass.initComponent.apply(this, arguments);
	}
});