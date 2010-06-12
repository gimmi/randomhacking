/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.RegionNormalSearchFormPanel = Ext.extend(Ext.form.FormPanel, {
	labelWidth: 100,
	border: false,
	padding: 10,
	initComponent: function () {
		this.items = [
			{ name: 'regionId', xtype: 'numberfield', fieldLabel: 'regionId' },
			{ name: 'regionDescription', xtype: 'textfield', fieldLabel: 'regionDescription' }
		];
		ExtMvc.RegionNormalSearchFormPanel.superclass.initComponent.apply(this, arguments);
	}
});