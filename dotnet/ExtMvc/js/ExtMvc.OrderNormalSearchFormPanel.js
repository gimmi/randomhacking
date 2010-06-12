/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.OrderNormalSearchFormPanel = Ext.extend(Ext.form.FormPanel, {
	labelWidth: 100,
	border: false,
	padding: 10,
	initComponent: function () {
		this.items = [
			{ name: 'orderId', xtype: 'numberfield', fieldLabel: 'orderId' },
			{ name: 'orderDate', xtype: 'datefield', fieldLabel: 'orderDate' },
			{ name: 'requiredDate', xtype: 'datefield', fieldLabel: 'requiredDate' },
			{ name: 'shippedDate', xtype: 'datefield', fieldLabel: 'shippedDate' },
			{ name: 'freight', xtype: 'numberfield', fieldLabel: 'freight' },
			{ name: 'shipName', xtype: 'textfield', fieldLabel: 'shipName' },
			{ name: 'shipAddress', xtype: 'textfield', fieldLabel: 'shipAddress' },
			{ name: 'shipCity', xtype: 'textfield', fieldLabel: 'shipCity' },
			{ name: 'shipRegion', xtype: 'textfield', fieldLabel: 'shipRegion' },
			{ name: 'shipPostalCode', xtype: 'textfield', fieldLabel: 'shipPostalCode' },
			{ name: 'shipCountry', xtype: 'textfield', fieldLabel: 'shipCountry' },
			{ name: 'customer', xtype: 'ExtMvc.CustomerField', fieldLabel: 'customer' },
			{ name: 'employee', xtype: 'ExtMvc.EmployeeField', fieldLabel: 'employee' },
			{ name: 'shipper', xtype: 'ExtMvc.ShipperField', fieldLabel: 'shipper' }
		];
		ExtMvc.OrderNormalSearchFormPanel.superclass.initComponent.apply(this, arguments);
	}
});