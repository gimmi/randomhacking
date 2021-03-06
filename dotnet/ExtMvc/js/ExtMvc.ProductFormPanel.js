/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ProductFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'ProductId', fieldLabel: 'ProductId', xtype: 'numberfield' },
				{ name: 'ProductName', fieldLabel: 'ProductName', xtype: 'textfield' },
				{ name: 'QuantityPerUnit', fieldLabel: 'QuantityPerUnit', xtype: 'textfield' },
				{ name: 'UnitPrice', fieldLabel: 'UnitPrice', xtype: 'numberfield' },
				{ name: 'UnitsInStock', fieldLabel: 'UnitsInStock', xtype: 'numberfield' },
				{ name: 'UnitsOnOrder', fieldLabel: 'UnitsOnOrder', xtype: 'numberfield' },
				{ name: 'ReorderLevel', fieldLabel: 'ReorderLevel', xtype: 'numberfield' },
				{ name: 'Discontinued', fieldLabel: 'Discontinued', xtype: 'checkbox' },
				{ name: 'Category', fieldLabel: 'Category', xtype: 'ExtMvc.Ns.CategoryField' }
			]
		});

		ExtMvc.ProductFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});