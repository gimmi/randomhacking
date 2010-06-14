/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ProductNormalSearchPanel = Ext.extend(Ext.Panel, {
	initComponent: function () {
		var _onGridPanelRowDblClick = function (grid, rowIndex, event) {
			var item = grid.getStore().getAt(rowIndex).data;
			this.fireEvent('itemselected', this, item);
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
		_gridPanel = new ExtMvc.ProductGridPanel({
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

		Ext.apply(this, {
			layout: 'border',
			border: false,
			items: [_searchFormPanel, _gridPanel],
			tbar: [
				{ text: 'Search', handler: _onSearchButtonClick, icon: '/images/zoom.png', cls: 'x-btn-text-icon', scope: this },
				{ text: 'New', handler: _onNewButtonClick, icon: '/images/add.png', cls: 'x-btn-text-icon', scope: this },
				{ text: 'Edit', handler: _onEditButtonClick, icon: '/images/pencil.png', cls: 'x-btn-text-icon', scope: this },
				{ text: 'Delete', handler: _onDeleteButtonClick, icon: '/images/delete.png', cls: 'x-btn-text-icon', scope: this }
			],
			getSelectedItem: function () {
				var sm = _gridPanel.getSelectionModel();
				return sm.getCount() > 0 ? sm.getSelected().data : null;
			}
		});

		ExtMvc.ProductNormalSearchPanel.superclass.initComponent.apply(this, arguments);

		this.addEvents('itemselected');
	}
});