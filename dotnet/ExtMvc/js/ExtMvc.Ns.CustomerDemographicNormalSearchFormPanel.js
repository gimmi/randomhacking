/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc.Ns */
"use strict";

Ext.namespace('ExtMvc.Ns');

ExtMvc.Ns.CustomerDemographicNormalSearchFormPanel = Ext.extend(Ext.form.FormPanel, {
	labelWidth: 100,
	border: false,
	padding: 10,
	initComponent: function () {
		this.items = [
			{ name: 'customerTypeId', xtype: 'textfield', fieldLabel: 'customerTypeId' },
			{ name: 'customerDesc', xtype: 'textfield', fieldLabel: 'customerDesc' }
		];
		ExtMvc.Ns.CustomerDemographicNormalSearchFormPanel.superclass.initComponent.apply(this, arguments);
	}
});