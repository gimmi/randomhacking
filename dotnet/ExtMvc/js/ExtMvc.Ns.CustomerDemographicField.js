/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc.Ns */
"use strict";
Ext.namespace('ExtMvc.Ns');

ExtMvc.Ns.CustomerDemographicField = Ext.extend(Ext.form.TriggerField, {
	editable: false,
	hideTrigger: true,
	initComponent: function () {
		var _this = this,
		_window,
		_selectedItem = null,
		_onEditEnded = function (sender, item) {
			_this.setValue(item);
			_window.hide();
		};

		Ext.apply(_this, {
			onTriggerClick: function () {
				_window = _window || new ExtMvc.Ns.CustomerDemographicEditWindow({
					closeAction: 'hide',
					listeners: {
						editended: _onEditEnded
					}
				});
				_window.setItem(_selectedItem);
				_window.show(this.getEl());
			},
			beforeDestroy: function () {
				if (_window) {
					_window.close();
				}
				return ExtMvc.Ns.CustomerDemographicField.superclass.beforeDestroy.apply(_this, arguments);
			},
			setValue: function (v) {
				_selectedItem = v;
				return ExtMvc.Ns.CustomerDemographicField.superclass.setValue.call(_this, ExtMvc.Ns.CustomerDemographic.toString(v));
			},
			getValue: function () {
				return _selectedItem;
			}
		});

		ExtMvc.Ns.CustomerDemographicField.superclass.initComponent.apply(_this, arguments);
	}
});

Ext.reg('ExtMvc.Ns.CustomerDemographicField', ExtMvc.Ns.CustomerDemographicField);