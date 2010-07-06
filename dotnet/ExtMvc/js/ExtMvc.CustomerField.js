/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.CustomerField = Ext.extend(Ext.form.ComboBox, {
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
				fields: [ 'StringId', 'Description' ]
			})
		});
// Test
		Ext.apply(_this, {
			store: _store,
			displayField: 'Description',
			pageSize: 10
		});

		ExtMvc.CustomerField.superclass.initComponent.apply(_this, arguments);
	}
});

Ext.reg('ExtMvc.CustomerField', ExtMvc.CustomerField);