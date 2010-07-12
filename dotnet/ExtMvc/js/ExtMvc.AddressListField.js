/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.AddressListField = Ext.extend(Ext.ux.ProxyField, {
	initComponent: function () {
		var _this = this,
		_onEditEnded = function (window, item) {
			var value = _this.getValue();
			if (value.indexOf(item) === -1) {
				value[value.length] = item;
			}
			window.close();
			_this.item.getStore().load();
		},
		_buildWindow = function () {
			return new ExtMvc.AddressEditWindow({
				listeners: {
					editended: _onEditEnded
				}
			});
		},
		_onNewButtonClick = function (button) {
			var window = _buildWindow();
			window.show(button.getEl());
		},
		_onEditButtonClick = function (button) {
			var sm, window;
			sm = _this.item.getSelectionModel();
			if (sm.getCount() > 0) {
				window = _buildWindow();
				window.setItem(sm.getSelected().data.$ref);
				window.show(button.getEl());
			}
		},
		_onDeleteButtonClick = function () {
			var sm = _this.item.getSelectionModel();
			if (sm.getCount() > 0) {
				_this.getValue().remove(sm.getSelected().data.$ref);
				_this.item.getStore().load();
			}
		};

		Ext.apply(_this, {
			item: new ExtMvc.AddressGridPanel({
				id: _this.id + '-gridpanel',
				store: new Ext.data.Store({
					autoDestroy: true,
					proxy: new Ext.data.MemoryProxy({ items: [] }),
					reader: new ExtMvc.AddressJsonReader()
				}),
				tbar: [
					{ text: 'New', handler: _onNewButtonClick, icon: 'images/add.png', cls: 'x-btn-text-icon' },
					{ text: 'Edit', handler: _onEditButtonClick, icon: 'images/pencil.png', cls: 'x-btn-text-icon' },
					{ text: 'Delete', handler: _onDeleteButtonClick, icon: 'images/delete.png', cls: 'x-btn-text-icon' }
				]
			}),
			setValue: function (v) {
				_this.item.getStore().proxy.data.items = v;
				_this.item.getStore().load();
				return ExtMvc.AddressListField.superclass.setValue.apply(_this, arguments);
			},
			getValue: function () {
				return _this.item.getStore().proxy.data.items;
			}
		});

		ExtMvc.AddressListField.superclass.initComponent.apply(_this, arguments);
	}
});

Ext.reg('ExtMvc.AddressListField', ExtMvc.AddressListField);