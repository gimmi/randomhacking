/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.TerritoryFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'TerritoryId', fieldLabel: 'TerritoryId', xtype: 'textfield' },
				{ name: 'TerritoryDescription', fieldLabel: 'TerritoryDescription', xtype: 'textfield' },
				{ name: 'Region', fieldLabel: 'Region', xtype: 'ExtMvc.RegionField' }
			]
		});

		ExtMvc.TerritoryFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});