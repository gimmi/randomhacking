/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.CustomerField = Ext.extend(Ext.ux.ProxyField, {
	initComponent: function () {
		var _this = this,
		_store = new Ext.data.Store({
			autoDestroy: true,
			proxy: new Rpc.JsonPostHttpProxy({
				url: 'Customer/AutocompleteSearch'
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
				store: _store //,mode: 'local'
			}),
			setValue: function (v) {
				_this.item.setValue(v.Description);
				return ExtMvc.CustomerField.superclass.setValue.apply(_this, arguments);
			},
			getValue: function () {
				var value = _this.item.getValue();
				return Ext.isEmpty(value) ? null : value;
			}
		});

		ExtMvc.CustomerField.superclass.initComponent.apply(_this, arguments);

		//_store.load();
	}
});

Ext.reg('ExtMvc.CustomerField', ExtMvc.CustomerField);