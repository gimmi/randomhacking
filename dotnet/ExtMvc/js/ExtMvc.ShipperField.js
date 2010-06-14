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
		_searchPanel,
		_selectedItem = null,
		_onSearchPanelItemSelected = function (sender, item) {
			_this.setValue(item);
			_window.hide();
		},
		_onSelectNoneButtonClick = function (button, event) {
			_this.setValue(null);
			_window.hide();
		},
		_onSelectButtonClick = function (button, event) {
			var selectedItem = _searchPanel.getSelectedItem();
			_this.setValue(selectedItem);
			_window.hide();
		},
		_onCancelButtonClick = function (button, event) {
			_window.hide();
		},
		_buildWindow = function () {
			_searchPanel = new ExtMvc.ShipperNormalSearchPanel({
				listeners: {
					itemselected: _onSearchPanelItemSelected
				}
			});
			_window = _window || new Ext.Window({
				title: 'Search Shipper',
				width: 600,
				height: 300,
				layout: 'fit',
				maximizable: true,
				closeAction: 'hide',
				items: _searchPanel,
				buttons: [
					{ text: 'Select None', handler: _onSelectNoneButtonClick },
					{ text: 'Select', handler: _onSelectButtonClick },
					{ text: 'Cancel', handler: _onCancelButtonClick }
				]
			});
		};

		Ext.apply(_this, {
			onTriggerClick: function () {
				if (!_window) {
					_buildWindow();
				}
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