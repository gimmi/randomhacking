/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.ShipperField = Ext.extend(Ext.form.TriggerField, {
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
				_window = _window || new ExtMvc.ShipperPickerWindow({
					listeners: {
						itemselected: _onItemSelected
					}
				});
				_window.show(this.getEl());
			},
			setValue: function (v) {
				_selectedItem = v;
				return ExtMvc.ShipperField.superclass.setValue.call(_this, ExtMvc.Shipper.toString(v));
			},
			getValue: function () {
				return _selectedItem;
			}
		});

		ExtMvc.ShipperField.superclass.initComponent.apply(this, arguments);
	}
});

Ext.reg('ExtMvc.ShipperField', ExtMvc.ShipperField);