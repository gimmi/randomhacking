/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.RegionFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'RegionId', fieldLabel: 'RegionId', xtype: 'numberfield' },
				{ name: 'RegionDescription', fieldLabel: 'RegionDescription', xtype: 'textfield' }
			]
		});

		ExtMvc.RegionFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});