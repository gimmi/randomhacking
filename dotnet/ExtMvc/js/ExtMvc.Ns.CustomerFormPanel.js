/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc.Ns */
"use strict";

Ext.namespace('ExtMvc.Ns');

ExtMvc.Ns.CustomerFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
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
					{ name: 'CustomerId', fieldLabel: 'CustomerId', xtype: 'textfield' },
					{ name: 'CompanyName', fieldLabel: 'CompanyName', xtype: 'textfield' },
					{ name: 'ContactName', fieldLabel: 'ContactName', xtype: 'textfield' },
					{ name: 'ContactTitle', fieldLabel: 'ContactTitle', xtype: 'textfield' },
					{ name: 'Address', fieldLabel: 'Address', xtype: 'textfield' },
					{ name: 'City', fieldLabel: 'City', xtype: 'textfield' },
					{ name: 'Region', fieldLabel: 'Region', xtype: 'textfield' },
					{ name: 'PostalCode', fieldLabel: 'PostalCode', xtype: 'textfield' },
					{ name: 'Country', fieldLabel: 'Country', xtype: 'textfield' },
					{ name: 'Phone', fieldLabel: 'Phone', xtype: 'textfield' },
					{ name: 'Fax', fieldLabel: 'Fax', xtype: 'textfield' }
				]
			}, {
				flex: 1,
				xtype: 'tabpanel',
				plain: true,
				border: false,
				activeTab: 0,
				deferredRender: false, // IMPORTANT! See http://www.extjs.com/deploy/dev/examples/form/dynamic.js
				items: [
					{ name: 'Customerdemographics', title: 'Customerdemographics', xtype: 'ExtMvc.Ns.CustomerDemographicListField' }
				]
			}]
		});

		ExtMvc.Ns.CustomerFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});