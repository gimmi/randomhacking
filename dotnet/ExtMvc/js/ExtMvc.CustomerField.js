/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.CustomerField = Ext.extend(Ext.form.TriggerField, {
	editable: false,
	hideTrigger: true,
	initComponent: function () {
		var _this = this,
		_window,
		_selectedItem = null,
		_onItemSelected = function (sender, item) {
			_this.setValue(item);
			_window.hide();
		};

		Ext.apply(_this, {
			onTriggerClick: function () {
				_window = _window || new ExtMvc.CustomerPickerWindow({
					listeners: {
						itemselected: _onItemSelected
					}
				});
				_window.show(this.getEl());
			},
			setValue: function (v) {
				_selectedItem = v;
				return ExtMvc.CustomerField.superclass.setValue.call(_this, ExtMvc.Customer.toString(v));
			},
			getValue: function () {
				return _selectedItem;
			}
		});

		ExtMvc.CustomerField.superclass.initComponent.apply(this, arguments);
	}
});

Ext.reg('ExtMvc.CustomerField', ExtMvc.CustomerField);