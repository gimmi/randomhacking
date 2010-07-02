/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.CategoryFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'CategoryId', fieldLabel: 'CategoryId', xtype: 'numberfield' },
				{ name: 'CategoryName', fieldLabel: 'CategoryName', xtype: 'textfield' },
				{ name: 'Description', fieldLabel: 'Description', xtype: 'textfield' }
			]
		});

		ExtMvc.CategoryFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});