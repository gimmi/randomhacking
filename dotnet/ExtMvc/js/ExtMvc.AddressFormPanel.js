/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.AddressFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'Name', fieldLabel: 'Name', xtype: 'textfield' },
				{ name: 'AddressString', fieldLabel: 'AddressString', xtype: 'textfield' },
				{ name: 'City', fieldLabel: 'City', xtype: 'textfield' },
				{ name: 'Region', fieldLabel: 'Region', xtype: 'textfield' },
				{ name: 'PostalCode', fieldLabel: 'PostalCode', xtype: 'textfield' },
				{ name: 'Country', fieldLabel: 'Country', xtype: 'textfield' }
			]
		});

		ExtMvc.AddressFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});