/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace("ExtMvc");

ExtMvc.RegionGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'RegionId', header: 'RegionId', xtype: 'numbercolumn' }, { dataIndex: 'RegionDescription', header: 'RegionDescription', xtype: 'gridcolumn' }
			]
		});
		ExtMvc.RegionGridPanel.superclass.initComponent.apply(this, arguments);
	}
});