/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.CategoryContainer = Ext.extend(Ext.Container, {
	initComponent: function () {
		var _this = this;
		Ext.apply(_this, {
			layout: 'vbox',
			layoutConfig: {
				align: 'stretch',
				pack: 'start'
			},
			items: [{
				layout: 'form',
				border: false,
				padding: 10,
				items: [
					{ name: 'StringId', xtype: 'hidden' },
					{ name: 'CategoryId', fieldLabel: 'CategoryId', xtype: 'numberfield' },
					{ name: 'CategoryName', fieldLabel: 'CategoryName', xtype: 'textfield' },
					{ name: 'Description', fieldLabel: 'Description', xtype: 'textfield' }
				]
			}]
		});

		ExtMvc.CategoryContainer.superclass.initComponent.apply(_this, arguments);
	}
});