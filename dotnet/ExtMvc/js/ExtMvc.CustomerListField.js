/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.CustomerListField = Ext.extend(Ext.form.Field, {
	initComponent: function () {
		var _this = this,
		_onNewButtonClick = function () {
			alert('click');
		},
		_onEditButtonClick = function () {
			alert('click');
		},
		_onDeleteButtonClick = function () {
			alert('click');
		},
		_cfg = Ext.copyTo({
			id: this.id + '-gridpanel',
			store: new Ext.data.Store({
				autoDestroy: true,
				proxy: new Ext.data.MemoryProxy({ items: [] }),
				reader: new ExtMvc.CustomerJsonReader()
			}),
			tbar: [
				{ text: 'New', handler: _onNewButtonClick, icon: 'images/add.png', cls: 'x-btn-text-icon' },
				{ text: 'Edit', handler: _onEditButtonClick, icon: 'images/pencil.png', cls: 'x-btn-text-icon' },
				{ text: 'Delete', handler: _onDeleteButtonClick, icon: 'images/delete.png', cls: 'x-btn-text-icon' }
			]
		}, _this.initialConfig, []),
		_gridPanel = new ExtMvc.CustomerGridPanel(_cfg);

		Ext.apply(_this, {
			onRender: function (ct, position) {
				// TODO This creates a hidden field above the grid. Check if this is good or not
				this.autoCreate = {
					id: _this.id,
					name: _this.name,
					type: 'hidden',
					tag: 'input'
				};
				ExtMvc.CustomerListField.superclass.onRender.call(_this, ct, position);
				_this.wrap = _this.el.wrap({ cls: 'x-form-field-wrap' });
				_this.resizeEl = _this.positionEl = _this.wrap;
				_gridPanel.render(_this.wrap);
			},
			onResize: function (w, h, aw, ah) {
				ExtMvc.CustomerListField.superclass.onResize.apply(_this, arguments);
				_gridPanel.setSize(w, h);
			},
			onEnable: function () {
				ExtMvc.CustomerListField.superclass.onEnable.apply(_this, arguments);
				_gridPanel.enable();
			},
			onDisable: function () {
				ExtMvc.CustomerListField.superclass.onDisable.apply(_this, arguments);
				_gridPanel.disable();
			},
			beforeDestroy: function () {
				Ext.destroy(_gridPanel);
				ExtMvc.CustomerListField.superclass.beforeDestroy.apply(_this, arguments);
			},
			setValue: function (v) {
				_gridPanel.getStore().proxy.data.items = v;
				_gridPanel.getStore().load();
				return ExtMvc.CustomerListField.superclass.setValue.apply(_this, arguments);
			},
			getValue: function () {
				return _gridPanel.getStore().proxy.data.items;
			}
		});

		ExtMvc.CustomerListField.superclass.initComponent.apply(_this, arguments);
	}
});

Ext.reg('ExtMvc.CustomerListField', ExtMvc.CustomerListField);