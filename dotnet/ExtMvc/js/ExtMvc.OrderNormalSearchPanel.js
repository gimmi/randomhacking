/*jslint white: true, onevar: true, browser: true, devel: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.OrderNormalSearchPanel = Ext.extend(Ext.Panel, {
	layout: 'border',
	border: false,
	initComponent: function () {
		var _onGridPanelRowDblClick = function (grid, rowIndex, event) {
			var item = grid.getStore().getAt(rowIndex).data;
			this.fireEvent('itemselected', this, item);
		},
		_searchFormPanel = new ExtMvc.OrderNormalSearchFormPanel({
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
				url: '/Order/SearchNormal'
			}),
			remoteSort: true,
			reader: new ExtMvc.OrderJsonReader()
		}),
		_gridPanel = new ExtMvc.OrderGridPanel({
			region: 'center',
			store: _store,
			bbar: new Ext.PagingToolbar({
				store: _store,
				displayInfo: true,
				pageSize: 25,
				prependButtons: true
			}),
			listeners: {
				rowdblclick: _onGridPanelRowDblClick,
				scope: this
			}
		}),
		_onSearchButtonClick = function (b, e) {
			var params = _searchFormPanel.getForm().getFieldValues();
			Ext.apply(_gridPanel.getStore().baseParams, params);
			_gridPanel.getStore().load({
				params: {
					start: 0,
					limit: _gridPanel.getBottomToolbar().pageSize
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
			alert('onDeleteButtonClick');
		};

		this.items = [_searchFormPanel, _gridPanel];

		this.tbar = [
			{ text: 'Search', handler: _onSearchButtonClick, icon: '/images/zoom.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'New', handler: _onNewButtonClick, icon: '/images/add.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'Edit', handler: _onEditButtonClick, icon: '/images/pencil.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'Delete', handler: _onDeleteButtonClick, icon: '/images/delete.png', cls: 'x-btn-text-icon', scope: this }
		];

		this.getSelectedItem = function () {
			var sm = _gridPanel.getSelectionModel();
			return sm.getCount() > 0 ? sm.getSelected().data : null;
		};

		this.addEvents('itemselected');

		ExtMvc.OrderNormalSearchPanel.superclass.initComponent.apply(this, arguments);
	}
});