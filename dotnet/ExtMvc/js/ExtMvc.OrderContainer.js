/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.OrderContainer = Ext.extend(Ext.Container, {
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
					{ name: 'OrderId', fieldLabel: 'OrderId', xtype: 'numberfield' },
					{ name: 'OrderDate', fieldLabel: 'OrderDate', xtype: 'datefield' },
					{ name: 'RequiredDate', fieldLabel: 'RequiredDate', xtype: 'datefield' },
					{ name: 'ShippedDate', fieldLabel: 'ShippedDate', xtype: 'datefield' },
					{ name: 'Freight', fieldLabel: 'Freight', xtype: 'numberfield' },
					{ name: 'ShipName', fieldLabel: 'ShipName', xtype: 'textfield' },
					{ name: 'ShipAddress', fieldLabel: 'ShipAddress', xtype: 'textfield' },
					{ name: 'ShipCity', fieldLabel: 'ShipCity', xtype: 'textfield' },
					{ name: 'ShipRegion', fieldLabel: 'ShipRegion', xtype: 'textfield' },
					{ name: 'ShipPostalCode', fieldLabel: 'ShipPostalCode', xtype: 'textfield' },
					{ name: 'ShipCountry', fieldLabel: 'ShipCountry', xtype: 'textfield' },
					{ name: 'Customer', fieldLabel: 'Customer', xtype: 'ExtMvc.CustomerField' },
					{ name: 'Employee', fieldLabel: 'Employee', xtype: 'ExtMvc.EmployeeField' }
				]
			}]
		});

		ExtMvc.OrderContainer.superclass.initComponent.apply(_this, arguments);
	}
});