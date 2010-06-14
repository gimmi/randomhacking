/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.CustomerDemographicPickerWindow = Ext.extend(Ext.Window, {
	initComponent: function () {
		var _this = this,
		_fireItemSelectedEvent = function (item) {
			_this.fireEvent('itemselected', _this, item);
		},
		_onGridPanelRowDblClick = function (grid, rowIndex, event) {
			var item = grid.getStore().getAt(rowIndex).data;
			_fireItemSelectedEvent(item);
		},
		_searchFormPanel = new ExtMvc.CustomerDemographicNormalSearchFormPanel({
			title: 'Search Filters',
			region: 'north',
			autoHeight: true,
			collapsible: true,
			collapsed: true,
			titleCollapse: true,
			floatable: false
		}),
		_store = new Ext.data.Store({
			autoDestroy: true,
			proxy: new Rpc.JsonPostHttpProxy({
				url: 'CustomerDemographic/SearchNormal'
			}),
			remoteSort: true,
			reader: new ExtMvc.CustomerDemographicJsonReader()
		}),
		_pagingToolbar = new Ext.PagingToolbar({
			store: _store,
			displayInfo: true,
			pageSize: 25,
			prependButtons: true
		}),
		_gridPanel = new ExtMvc.CustomerDemographicGridPanel({
			region: 'center',
			store: _store,
			bbar: _pagingToolbar,
			listeners: {
				rowdblclick: _onGridPanelRowDblClick
			}
		}),
		_onSearchButtonClick = function (b, e) {
			var params = _searchFormPanel.getForm().getFieldValues();
			Ext.apply(_gridPanel.getStore().baseParams, params);
			_gridPanel.getStore().load({
				params: {
					start: 0,
					limit: _pagingToolbar.pageSize
				}
			});
		},
		_onSelectNoneButtonClick = function (button, event) {
			_fireItemSelectedEvent(null);
		},
		_onSelectButtonClick = function (button, event) {
			var sm = _gridPanel.getSelectionModel();
			var selectedItem = sm.getCount() > 0 ? sm.getSelected().data : null;
			_fireItemSelectedEvent(selectedItem);
		};

		Ext.apply(_this, {
			title: 'Pick a CustomerDemographic',
			width: 600,
			height: 300,
			layout: 'border',
			maximizable: true,
			closeAction: 'hide',
			items: [_searchFormPanel, _gridPanel],
			tbar: [
				{ text: 'Search', handler: _onSearchButtonClick, icon: 'images/zoom.png', cls: 'x-btn-text-icon' }
			],
			buttons: [
				{ text: 'Select None', handler: _onSelectNoneButtonClick },
				{ text: 'Select', handler: _onSelectButtonClick }
			]
		});

		ExtMvc.CustomerDemographicPickerWindow.superclass.initComponent.apply(_this, arguments);

		_this.addEvents('itemselected');
	}
});