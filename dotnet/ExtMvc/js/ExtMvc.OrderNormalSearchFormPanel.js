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
			{ name: 'address', xtype: 'ExtMvc.AddressField', fieldLabel: 'address' },
			{ name: 'customer', xtype: 'ExtMvc.Ns.CustomerField', fieldLabel: 'customer' },
			{ name: 'employee', xtype: 'ExtMvc.Ns.EmployeeField', fieldLabel: 'employee' },
			{ name: 'shipper', xtype: 'ExtMvc.ShipperField', fieldLabel: 'shipper' }
		];
		ExtMvc.OrderNormalSearchFormPanel.superclass.initComponent.apply(this, arguments);
	}
});