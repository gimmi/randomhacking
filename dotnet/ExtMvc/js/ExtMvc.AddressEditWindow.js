/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.AddressEditWindow = Ext.extend(Ext.Window, {
	initComponent: function () {
		var _this = this,
		_item = null,
		_formPanel = new ExtMvc.AddressFormPanel(),
		_fireEditEndedEvent = function (item) {
			_this.fireEvent('editended', _this, item);
		},
		_onOkButtonClick = function () {
			var fieldValues = _formPanel.getForm().getFieldValues();
			_item = Ext.apply(_item || {}, fieldValues);
			_fireEditEndedEvent(_item);
		};

		Ext.apply(_this, {
			title: 'Edit Address',
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
				_item = item;
				_formPanel.getForm().setValues(_item);
			}
		});

		ExtMvc.AddressEditWindow.superclass.initComponent.apply(_this, arguments);

		_this.addEvents('editended');
	}
});