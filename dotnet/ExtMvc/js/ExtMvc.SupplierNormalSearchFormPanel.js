/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.SupplierNormalSearchFormPanel = Ext.extend(Ext.form.FormPanel, {
	labelWidth: 100,
	border: false,
	padding: 10,
	initComponent: function () {
		this.items = [
			{ name: 'supplierId', xtype: 'numberfield', fieldLabel: 'supplierId' },
			{ name: 'companyName', xtype: 'textfield', fieldLabel: 'companyName' },
			{ name: 'contactName', xtype: 'textfield', fieldLabel: 'contactName' },
			{ name: 'contactTitle', xtype: 'textfield', fieldLabel: 'contactTitle' },
			{ name: 'address', xtype: 'textfield', fieldLabel: 'address' },
			{ name: 'city', xtype: 'textfield', fieldLabel: 'city' },
			{ name: 'region', xtype: 'textfield', fieldLabel: 'region' },
			{ name: 'postalCode', xtype: 'textfield', fieldLabel: 'postalCode' },
			{ name: 'country', xtype: 'textfield', fieldLabel: 'country' },
			{ name: 'phone', xtype: 'textfield', fieldLabel: 'phone' },
			{ name: 'fax', xtype: 'textfield', fieldLabel: 'fax' },
			{ name: 'homePage', xtype: 'textfield', fieldLabel: 'homePage' }
		];
		ExtMvc.SupplierNormalSearchFormPanel.superclass.initComponent.apply(this, arguments);
	}
});