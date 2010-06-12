/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.OrderDetailSearchFormPanel = Ext.extend(Ext.form.FormPanel, {
	labelWidth: 100,
	border: false,
	padding: 10,
	initComponent: function () {
		this.items = [
			{ name: 'orderId', xtype: 'numberfield', fieldLabel: 'orderId' },
			{ name: 'productId', xtype: 'numberfield', fieldLabel: 'productId' },
			{ name: 'unitPrice', xtype: 'numberfield', fieldLabel: 'unitPrice' },
			{ name: 'quantity', xtype: 'numberfield', fieldLabel: 'quantity' },
			{ name: 'discount', xtype: 'numberfield', fieldLabel: 'discount' }
		];
		ExtMvc.OrderDetailSearchFormPanel.superclass.initComponent.apply(this, arguments);
	}
});