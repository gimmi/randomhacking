/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ShipperNormalSearchFormPanel = Ext.extend(Ext.form.FormPanel, {
	labelWidth: 100,
	border: false,
	padding: 10,
	initComponent: function () {
		this.items = [
			{ name: 'shipperId', xtype: 'numberfield', fieldLabel: 'shipperId' },
			{ name: 'companyName', xtype: 'textfield', fieldLabel: 'companyName' },
			{ name: 'phone', xtype: 'textfield', fieldLabel: 'phone' }
		];
		ExtMvc.ShipperNormalSearchFormPanel.superclass.initComponent.apply(this, arguments);
	}
});