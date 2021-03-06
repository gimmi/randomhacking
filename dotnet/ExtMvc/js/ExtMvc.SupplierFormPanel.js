/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.SupplierFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'SupplierId', fieldLabel: 'SupplierId', xtype: 'numberfield' },
				{ name: 'CompanyName', fieldLabel: 'CompanyName', xtype: 'textfield' },
				{ name: 'ContactName', fieldLabel: 'ContactName', xtype: 'textfield' },
				{ name: 'ContactTitle', fieldLabel: 'ContactTitle', xtype: 'textfield' },
				{ name: 'Address', fieldLabel: 'Address', xtype: 'textfield' },
				{ name: 'City', fieldLabel: 'City', xtype: 'textfield' },
				{ name: 'Region', fieldLabel: 'Region', xtype: 'textfield' },
				{ name: 'PostalCode', fieldLabel: 'PostalCode', xtype: 'textfield' },
				{ name: 'Country', fieldLabel: 'Country', xtype: 'textfield' },
				{ name: 'Phone', fieldLabel: 'Phone', xtype: 'textfield' },
				{ name: 'Fax', fieldLabel: 'Fax', xtype: 'textfield' },
				{ name: 'HomePage', fieldLabel: 'HomePage', xtype: 'textfield' }
			]
		});

		ExtMvc.SupplierFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});