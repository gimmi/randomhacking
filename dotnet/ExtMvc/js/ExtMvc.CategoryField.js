/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.CategoryField = Ext.extend(Ext.form.TriggerField, {
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
			_searchPanel = new ExtMvc.CategoryNormalSearchPanel({
				listeners: {
					itemselected: _onSearchPanelItemSelected
				}
			});
			_window = _window || new Ext.Window({
				title: 'Search Category',
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
				return ExtMvc.CategoryField.superclass.setValue.call(_this, ExtMvc.Category.toString(v));
			},
			getValue: function () {
				return _selectedItem;
			}
		});

		ExtMvc.CategoryField.superclass.initComponent.apply(this, arguments);
	}
});

Ext.reg('ExtMvc.CategoryField', ExtMvc.CategoryField);