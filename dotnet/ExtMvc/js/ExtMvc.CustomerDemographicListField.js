/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.CustomerDemographicListField = Ext.extend(Ext.form.Field, {
	initComponent: function () {
		var _this = this,
		_gridPanel,
		_getSelectedRecord = function () {
			var sm = _gridPanel.getSelectionModel();
			return sm.getCount() > 0 ? sm.getSelected() : null;
		},
		_onItemAccepted = function (window, item) {
			// var selectedRecord = _getSelectedRecord();
			// Ext.apply(_getSelectedRecord().data, item);
			window.close();
			_gridPanel.getStore().load();
		},
		_buildWindow = function () {
			return new ExtMvc.CustomerDemographicEditWindow({
				listeners: {
					itemaccepted: _onItemAccepted
				}
			});
		},
		_onNewButtonClick = function (button) {
			var window = _buildWindow();
			window.show(button.getEl());
		},
		_onEditButtonClick = function (button) {
			var selectedRecord, window;
			selectedRecord = _getSelectedRecord();
			if (selectedRecord) {
				window = _buildWindow();
				window.setItem(selectedRecord.data);
				window.show(button.getEl());
			}
		},
		_onDeleteButtonClick = function () {
			// TODO
		},
		_onGridPanelRowDblClick = function (grid, rowIndex, event) {
			var selectedItem, window;
			selectedItem = grid.getStore().getAt(rowIndex).data;
			window = _buildWindow();
			window.setItem(selectedItem);
			window.show();
		};

		_gridPanel = new ExtMvc.CustomerDemographicGridPanel(Ext.copyTo({
			id: _this.id + '-gridpanel',
			store: new Ext.data.Store({
				autoDestroy: true,
				proxy: new Ext.data.MemoryProxy({ items: [] }),
				reader: new ExtMvc.CustomerDemographicJsonReader()
			}),
			tbar: [
				{ text: 'New', handler: _onNewButtonClick, icon: 'images/add.png', cls: 'x-btn-text-icon' },
				{ text: 'Edit', handler: _onEditButtonClick, icon: 'images/pencil.png', cls: 'x-btn-text-icon' },
				{ text: 'Delete', handler: _onDeleteButtonClick, icon: 'images/delete.png', cls: 'x-btn-text-icon' }
			]
		}, _this.initialConfig, []));

		Ext.apply(_this, {
			onRender: function (ct, position) {
				// TODO This creates a hidden field above the grid. Check if this is good or not
				this.autoCreate = {
					id: _this.id,
					name: _this.name,
					type: 'hidden',
					tag: 'input'
				};
				ExtMvc.CustomerDemographicListField.superclass.onRender.call(_this, ct, position);
				_this.wrap = _this.el.wrap({ cls: 'x-form-field-wrap' });
				_this.resizeEl = _this.positionEl = _this.wrap;
				_gridPanel.render(_this.wrap);
			},
			onResize: function (w, h, aw, ah) {
				ExtMvc.CustomerDemographicListField.superclass.onResize.apply(_this, arguments);
				_gridPanel.setSize(w, h);
			},
			onEnable: function () {
				ExtMvc.CustomerDemographicListField.superclass.onEnable.apply(_this, arguments);
				_gridPanel.enable();
			},
			onDisable: function () {
				ExtMvc.CustomerDemographicListField.superclass.onDisable.apply(_this, arguments);
				_gridPanel.disable();
			},
			beforeDestroy: function () {
				Ext.destroy(_gridPanel);
				ExtMvc.CustomerDemographicListField.superclass.beforeDestroy.apply(_this, arguments);
			},
			setValue: function (v) {
				_gridPanel.getStore().proxy.data.items = v;
				_gridPanel.getStore().load();
				return ExtMvc.CustomerDemographicListField.superclass.setValue.apply(_this, arguments);
			},
			getValue: function () {
				return _gridPanel.getStore().proxy.data.items;
			}
		});

		ExtMvc.CustomerDemographicListField.superclass.initComponent.apply(_this, arguments);
	}
});

Ext.reg('ExtMvc.CustomerDemographicListField', ExtMvc.CustomerDemographicListField);