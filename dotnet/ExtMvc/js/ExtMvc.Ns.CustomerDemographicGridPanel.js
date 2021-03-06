/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc.Ns */
"use strict";

Ext.namespace("ExtMvc.Ns");

ExtMvc.Ns.CustomerDemographicGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'CustomerTypeId', header: 'CustomerTypeId', xtype: 'gridcolumn' }, { dataIndex: 'CustomerDesc', header: 'CustomerDesc', xtype: 'gridcolumn' }
			]
		});
		ExtMvc.Ns.CustomerDemographicGridPanel.superclass.initComponent.apply(this, arguments);
	}
});