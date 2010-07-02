/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ShipperNormalSearchPanel = Ext.extend(Ext.Panel, {
	initComponent: function () {
		var _this = this,
		_fireEditItemEvent = function (item) {
			_this.fireEvent('edititem', _this, item);
		},
		_fireNewItemEvent = function () {
			_this.fireEvent('newitem', _this);
		},
		_onGridPanelRowDblClick = function (grid, rowIndex, event) {
			var item = grid.getStore().getAt(rowIndex).data;
			_fireEditItemEvent(item);
		},
		_searchFormPanel = new ExtMvc.ShipperNormalSearchFormPanel({
			title: 'Search Filters',
			region: 'north',
			autoHeight: true,
			collapsible: true,
			collapsed: true,
			titleCollapse: true,
			floatable: false
		}),
		_onStartLoad = function () {
			_this.el.mask('Loading...', 'x-mask-loading');
		},
		_onEndLoad = function () {
			_this.el.unmask();
		},
		_store = new Ext.data.Store({
			autoDestroy: true,
			proxy: new Rpc.JsonPostHttpProxy({
				url: 'Shipper/SearchNormal'
			}),
			remoteSort: true,
			reader: new ExtMvc.ShipperJsonReader(),
			listeners: {
				beforeload: _onStartLoad,
				load: _onEndLoad,
				exception: _onEndLoad
			}
		}),
		_pagingToolbar = new Ext.PagingToolbar({
			store: _store,
			displayInfo: true,
			pageSize: 25,
			prependButtons: true
		}),
		_gridPanel = new ExtMvc.ShipperGridPanel({
			region: 'center',
			store: _store,
			bbar: _pagingToolbar,
			listeners: {
				rowdblclick: _onGridPanelRowDblClick
			}
		}),
		_getSelectedItem = function () {
			var sm = _gridPanel.getSelectionModel();
			return sm.getCount() > 0 ? sm.getSelected().data : null;
		},
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
		_onNewButtonClick = function () {
			_fireNewItemEvent();
		},
		_onEditButtonClick = function () {
			var selectedItem = _getSelectedItem();
			if (!selectedItem) {
				return;
			}
			_fireEditItemEvent(selectedItem);
		},
		_onDeleteButtonClick = function () {
			var selectedItem = _getSelectedItem();
			if (!selectedItem) {
				return;
			}
			Ext.MessageBox.confirm('Delete', 'Are you sure?', function (buttonId) {
				if (buttonId !== 'yes') {
					return;
				}
				Rpc.call({
					url: 'Shipper/Delete',
					params: { stringId: selectedItem.StringId },
					success: function (result) {
						_pagingToolbar.doRefresh();
					}
				});
			});
		};

		Ext.apply(_this, {
			layout: 'border',
			border: false,
			items: [_searchFormPanel, _gridPanel],
			tbar: [
				{ text: 'Search', handler: _onSearchButtonClick, icon: 'images/zoom.png', cls: 'x-btn-text-icon' },
				{ text: 'New', handler: _onNewButtonClick, icon: 'images/add.png', cls: 'x-btn-text-icon' },
				{ text: 'Edit', handler: _onEditButtonClick, icon: 'images/pencil.png', cls: 'x-btn-text-icon' },
				{ text: 'Delete', handler: _onDeleteButtonClick, icon: 'images/delete.png', cls: 'x-btn-text-icon' }
			]
		});

		ExtMvc.ShipperNormalSearchPanel.superclass.initComponent.apply(_this, arguments);

		_this.addEvents('edititem', 'newitem');
	}
});