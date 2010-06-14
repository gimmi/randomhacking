/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ProductNormalSearchPanel = Ext.extend(Ext.Panel, {
	initComponent: function () {
		var _this = this,
		_onGridPanelRowDblClick = function (grid, rowIndex, event) {
			var item = grid.getStore().getAt(rowIndex).data;
			_this.fireEvent('itemselected', _this, item);
		},
		_searchFormPanel = new ExtMvc.ProductNormalSearchFormPanel({
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
				url: '/Product/SearchNormal'
			}),
			remoteSort: true,
			reader: new ExtMvc.ProductJsonReader()
		}),
		_pagingToolbar = new Ext.PagingToolbar({
			store: _store,
			displayInfo: true,
			pageSize: 25,
			prependButtons: true
		}),
		_gridPanel = new ExtMvc.ProductGridPanel({
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
		_onNewButtonClick = function () {
			alert('onNewButtonClick');
		},
		_onEditButtonClick = function () {
			alert('onEditButtonClick');
		},
		_onDeleteButtonClick = function () {
			var selectedItem = _this.getSelectedItem();
			if (!selectedItem) {
				return;
			}
			Ext.MessageBox.confirm('Delete', 'Are you sure?', function (buttonId) {
				if (buttonId !== 'yes') {
					return;
				}
				Rpc.call({
					url: '/Product/Delete',
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
				{ text: 'Search', handler: _onSearchButtonClick, icon: '/images/zoom.png', cls: 'x-btn-text-icon' },
				{ text: 'New', handler: _onNewButtonClick, icon: '/images/add.png', cls: 'x-btn-text-icon' },
				{ text: 'Edit', handler: _onEditButtonClick, icon: '/images/pencil.png', cls: 'x-btn-text-icon' },
				{ text: 'Delete', handler: _onDeleteButtonClick, icon: '/images/delete.png', cls: 'x-btn-text-icon' }
			],
			getSelectedItem: function () {
				var sm = _gridPanel.getSelectionModel();
				return sm.getCount() > 0 ? sm.getSelected().data : null;
			}
		});

		ExtMvc.ProductNormalSearchPanel.superclass.initComponent.apply(_this, arguments);

		_this.addEvents('itemselected');
	}
});