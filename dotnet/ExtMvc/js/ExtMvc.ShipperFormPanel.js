/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ShipperFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'ShipperId', fieldLabel: 'ShipperId', xtype: 'numberfield' },
				{ name: 'CompanyName', fieldLabel: 'CompanyName', xtype: 'textfield' },
				{ name: 'Phone', fieldLabel: 'Phone', xtype: 'textfield' }
			]
		});

		ExtMvc.ShipperFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});