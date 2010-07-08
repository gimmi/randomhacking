/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.MainViewport = Ext.extend(Ext.Viewport, {
	layout: 'border',
	initComponent: function () {
		var _this = this,
		_treePanel,
		_tabPanel = new Ext.TabPanel({
			activeTab: 0,
			region: 'center',
			items: [{
				xtype: 'panel',
				title: 'Welcome Page'
			}]
		});

		function _openTab (title, Constructor) {
			var tab;

			Ext.each(_tabPanel.items.items, function (item) {
				if (item.title === title) {
					tab = item;
					return false;
				}
			});

			tab = tab || _tabPanel.add(new Constructor({
				title: title,
				closable: true
			}));

			tab.show();
			return tab;
		}

						function _onCategoryNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Category.toString(item);
							editTab = _openTab('Category ' + description, ExtMvc.CategoryEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onCategoryNewItem (sender) {
							_openTab('New Category', ExtMvc.CategoryEditPanel);
						}
						function _onCategoryNormalClick (sender, item) {
							var searchTab = _openTab('Search Category Normal', ExtMvc.CategoryNormalSearchPanel);
							searchTab.on('edititem', _onCategoryNormalEditItem);
							searchTab.on('newitem', _onCategoryNewItem);
						}
						
						function _onCustomerNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Customer.toString(item);
							editTab = _openTab('Customer ' + description, ExtMvc.CustomerEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onCustomerNewItem (sender) {
							_openTab('New Customer', ExtMvc.CustomerEditPanel);
						}
						function _onCustomerNormalClick (sender, item) {
							var searchTab = _openTab('Search Customer Normal', ExtMvc.CustomerNormalSearchPanel);
							searchTab.on('edititem', _onCustomerNormalEditItem);
							searchTab.on('newitem', _onCustomerNewItem);
						}
						
						function _onEmployeeNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Employee.toString(item);
							editTab = _openTab('Employee ' + description, ExtMvc.EmployeeEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onEmployeeNewItem (sender) {
							_openTab('New Employee', ExtMvc.EmployeeEditPanel);
						}
						function _onEmployeeNormalClick (sender, item) {
							var searchTab = _openTab('Search Employee Normal', ExtMvc.EmployeeNormalSearchPanel);
							searchTab.on('edititem', _onEmployeeNormalEditItem);
							searchTab.on('newitem', _onEmployeeNewItem);
						}
						
						function _onOrderDetailEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.OrderDetail.toString(item);
							editTab = _openTab('OrderDetail ' + description, ExtMvc.OrderDetailEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onOrderDetailNewItem (sender) {
							_openTab('New OrderDetail', ExtMvc.OrderDetailEditPanel);
						}
						function _onOrderDetailClick (sender, item) {
							var searchTab = _openTab('Search OrderDetail', ExtMvc.OrderDetailSearchPanel);
							searchTab.on('edititem', _onOrderDetailEditItem);
							searchTab.on('newitem', _onOrderDetailNewItem);
						}
						
						function _onOrderNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Order.toString(item);
							editTab = _openTab('Order ' + description, ExtMvc.OrderEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onOrderNewItem (sender) {
							_openTab('New Order', ExtMvc.OrderEditPanel);
						}
						function _onOrderNormalClick (sender, item) {
							var searchTab = _openTab('Search Order Normal', ExtMvc.OrderNormalSearchPanel);
							searchTab.on('edititem', _onOrderNormalEditItem);
							searchTab.on('newitem', _onOrderNewItem);
						}
						
						function _onProductNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Product.toString(item);
							editTab = _openTab('Product ' + description, ExtMvc.ProductEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onProductNewItem (sender) {
							_openTab('New Product', ExtMvc.ProductEditPanel);
						}
						function _onProductNormalClick (sender, item) {
							var searchTab = _openTab('Search Product Normal', ExtMvc.ProductNormalSearchPanel);
							searchTab.on('edititem', _onProductNormalEditItem);
							searchTab.on('newitem', _onProductNewItem);
						}
						
						function _onRegionNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Region.toString(item);
							editTab = _openTab('Region ' + description, ExtMvc.RegionEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onRegionNewItem (sender) {
							_openTab('New Region', ExtMvc.RegionEditPanel);
						}
						function _onRegionNormalClick (sender, item) {
							var searchTab = _openTab('Search Region Normal', ExtMvc.RegionNormalSearchPanel);
							searchTab.on('edititem', _onRegionNormalEditItem);
							searchTab.on('newitem', _onRegionNewItem);
						}
						
						function _onShipperNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Shipper.toString(item);
							editTab = _openTab('Shipper ' + description, ExtMvc.ShipperEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onShipperNewItem (sender) {
							_openTab('New Shipper', ExtMvc.ShipperEditPanel);
						}
						function _onShipperNormalClick (sender, item) {
							var searchTab = _openTab('Search Shipper Normal', ExtMvc.ShipperNormalSearchPanel);
							searchTab.on('edititem', _onShipperNormalEditItem);
							searchTab.on('newitem', _onShipperNewItem);
						}
						
						function _onSupplierNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Supplier.toString(item);
							editTab = _openTab('Supplier ' + description, ExtMvc.SupplierEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onSupplierNewItem (sender) {
							_openTab('New Supplier', ExtMvc.SupplierEditPanel);
						}
						function _onSupplierNormalClick (sender, item) {
							var searchTab = _openTab('Search Supplier Normal', ExtMvc.SupplierNormalSearchPanel);
							searchTab.on('edititem', _onSupplierNormalEditItem);
							searchTab.on('newitem', _onSupplierNewItem);
						}
						
						function _onTerritoryNormalEditItem (sender, item) {
							var editTab, description;
							description = ExtMvc.Territory.toString(item);
							editTab = _openTab('Territory ' + description, ExtMvc.TerritoryEditPanel);
							editTab.loadItem(item.StringId);
						}
						function _onTerritoryNewItem (sender) {
							_openTab('New Territory', ExtMvc.TerritoryEditPanel);
						}
						function _onTerritoryNormalClick (sender, item) {
							var searchTab = _openTab('Search Territory Normal', ExtMvc.TerritoryNormalSearchPanel);
							searchTab.on('edititem', _onTerritoryNormalEditItem);
							searchTab.on('newitem', _onTerritoryNewItem);
						}
						
				
		_treePanel = new Ext.tree.TreePanel({
			title: 'Main Menu',
			region: 'west',
			split: true,
			collapsible: true,
			width: 200,
			rootVisible: false,
			root: {
				text: 'Root Node',
				children: [{
					text: 'Category',
					children: [{
						text: 'Search Category Normal',
						leaf: true,
						listeners: {
							click: _onCategoryNormalClick
						}
					}, {
						text: 'Create Category',
						leaf: true,
						listeners: {
							click: _onCategoryNewItem
						}
					}]
				}, {
					text: 'Customer',
					children: [{
						text: 'Search Customer Normal',
						leaf: true,
						listeners: {
							click: _onCustomerNormalClick
						}
					}, {
						text: 'Create Customer',
						leaf: true,
						listeners: {
							click: _onCustomerNewItem
						}
					}]
				}, {
					text: 'Employee',
					children: [{
						text: 'Search Employee Normal',
						leaf: true,
						listeners: {
							click: _onEmployeeNormalClick
						}
					}, {
						text: 'Create Employee',
						leaf: true,
						listeners: {
							click: _onEmployeeNewItem
						}
					}]
				}, {
					text: 'OrderDetail',
					children: [{
						text: 'Search OrderDetail',
						leaf: true,
						listeners: {
							click: _onOrderDetailClick
						}
					}, {
						text: 'Create OrderDetail',
						leaf: true,
						listeners: {
							click: _onOrderDetailNewItem
						}
					}]
				}, {
					text: 'Order',
					children: [{
						text: 'Search Order Normal',
						leaf: true,
						listeners: {
							click: _onOrderNormalClick
						}
					}, {
						text: 'Create Order',
						leaf: true,
						listeners: {
							click: _onOrderNewItem
						}
					}]
				}, {
					text: 'Product',
					children: [{
						text: 'Search Product Normal',
						leaf: true,
						listeners: {
							click: _onProductNormalClick
						}
					}, {
						text: 'Create Product',
						leaf: true,
						listeners: {
							click: _onProductNewItem
						}
					}]
				}, {
					text: 'Region',
					children: [{
						text: 'Search Region Normal',
						leaf: true,
						listeners: {
							click: _onRegionNormalClick
						}
					}, {
						text: 'Create Region',
						leaf: true,
						listeners: {
							click: _onRegionNewItem
						}
					}]
				}, {
					text: 'Shipper',
					children: [{
						text: 'Search Shipper Normal',
						leaf: true,
						listeners: {
							click: _onShipperNormalClick
						}
					}, {
						text: 'Create Shipper',
						leaf: true,
						listeners: {
							click: _onShipperNewItem
						}
					}]
				}, {
					text: 'Supplier',
					children: [{
						text: 'Search Supplier Normal',
						leaf: true,
						listeners: {
							click: _onSupplierNormalClick
						}
					}, {
						text: 'Create Supplier',
						leaf: true,
						listeners: {
							click: _onSupplierNewItem
						}
					}]
				}, {
					text: 'Territory',
					children: [{
						text: 'Search Territory Normal',
						leaf: true,
						listeners: {
							click: _onTerritoryNormalClick
						}
					}, {
						text: 'Create Territory',
						leaf: true,
						listeners: {
							click: _onTerritoryNewItem
						}
					}]
				}]
			},
			loader: {}
		});

		this.items = [_treePanel, _tabPanel];

		ExtMvc.MainViewport.superclass.initComponent.apply(this, arguments);
	}
});