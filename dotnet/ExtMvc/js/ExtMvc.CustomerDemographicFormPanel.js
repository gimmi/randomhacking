/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.CustomerDemographicFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			layout: 'fit',
			items: new ExtMvc.CustomerDemographicContainer(),

			setItem: function (item) {
				_this.getForm().setValues(item);
			},
			getUpdatedItem: function () {
				return _this.getForm().getFieldValues();
			}

		});

		ExtMvc.CustomerDemographicFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});