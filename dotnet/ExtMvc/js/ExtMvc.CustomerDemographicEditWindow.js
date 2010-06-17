/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.CustomerDemographicEditWindow = Ext.extend(Ext.Window, {
	initComponent: function () {
		var _this = this,
		_formPanel = new ExtMvc.CustomerDemographicFormPanel(),
		_fireItemAcceptedEvent = function (item) {
			_this.fireEvent('itemaccepted', _this, item);
		},
		_onOkButtonClick = function () {
			var item = _formPanel.getForm().getFieldValues();
			_fireItemAcceptedEvent(item);
		};

		Ext.apply(_this, {
			title: 'Edit CustomerDemographic',
			width: 600,
			height: 300,
			layout: 'fit',
			maximizable: true,
			modal: true,
			items: _formPanel,
			buttons: [
				{ text: 'Ok', handler: _onOkButtonClick }
			],
			setItem: function (item) {
				_formPanel.getForm().setValues(item);
			}
		});

		ExtMvc.CustomerDemographicEditWindow.superclass.initComponent.apply(_this, arguments);

		_this.addEvents('itemaccepted');
	}
});