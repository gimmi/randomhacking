/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc.Ns */
"use strict";

Ext.namespace('ExtMvc.Ns');

ExtMvc.Ns.CustomerDemographicFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'CustomerTypeId', fieldLabel: 'CustomerTypeId', xtype: 'textfield' },
				{ name: 'CustomerDesc', fieldLabel: 'CustomerDesc', xtype: 'textfield' }
			]
		});

		ExtMvc.Ns.CustomerDemographicFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});