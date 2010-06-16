/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.TerritoryContainer = Ext.extend(Ext.Container, {
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
					{ name: 'TerritoryId', fieldLabel: 'TerritoryId', xtype: 'textfield' },
					{ name: 'TerritoryDescription', fieldLabel: 'TerritoryDescription', xtype: 'textfield' },
					{ name: 'Region', fieldLabel: 'Region', xtype: 'ExtMvc.RegionField' }
				]
			}]
		});

		ExtMvc.TerritoryContainer.superclass.initComponent.apply(_this, arguments);
	}
});