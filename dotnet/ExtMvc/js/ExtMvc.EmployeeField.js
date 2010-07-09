/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.EmployeeField = Ext.extend(Ext.ux.ProxyField, {
	initComponent: function () {
		var _this = this,
		_valueProxy = new Rpc.LoadableValue({
			getValue: function () {
				var value = _this.item.getValue();
				return Ext.isEmpty(value) ? null : value;
			},
			setValue: function (v) {
				_this.item.setValue(v.Description);
				ExtMvc.EmployeeField.superclass.setValue.apply(_this, arguments);
			}
		}),
		_store = new Ext.data.Store({
			autoDestroy: true,
			proxy: new Rpc.JsonPostHttpProxy({
				url: 'Employee/ComboSearch'
			}),
			reader: new Rpc.JsonReader({
				root: 'items',
				idProperty: 'StringId',
				fields: ['StringId', 'Description', {
					name: '$ref',
					convert: function (v, r) {
						return r;
					}
				}]
			})
		});

		Ext.apply(_this, {
			item: new Ext.form.ComboBox({
				forceSelection: true,
				typeAhead: true,
				minChars: 0,
				displayField: 'Description',
				valueField: '$ref',
				mode: 'local',
				disabled: true,
				store: _store
			}),
			setValue: _valueProxy.setValue,
			getValue: _valueProxy.getValue,
		});

		ExtMvc.EmployeeField.superclass.initComponent.apply(_this, arguments);

		_store.load({
			callback: function () {
				_this.item.enable();
				_valueProxy.notifyLoadComplete();
			}
		});
	}
});

Ext.reg('ExtMvc.EmployeeField', ExtMvc.EmployeeField);