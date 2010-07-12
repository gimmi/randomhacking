/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ProductNormalSearchFormPanel = Ext.extend(Ext.form.FormPanel, {
	labelWidth: 100,
	border: false,
	padding: 10,
	initComponent: function () {
		this.items = [
			{ name: 'productId', xtype: 'numberfield', fieldLabel: 'productId' },
			{ name: 'productName', xtype: 'textfield', fieldLabel: 'productName' },
			{ name: 'discontinued', xtype: 'checkbox', fieldLabel: 'discontinued' },
			{ name: 'category', xtype: 'ExtMvc.Ns.CategoryField', fieldLabel: 'category' },
			{ name: 'supplier', xtype: 'ExtMvc.SupplierField', fieldLabel: 'supplier' }
		];
		ExtMvc.ProductNormalSearchFormPanel.superclass.initComponent.apply(this, arguments);
	}
});