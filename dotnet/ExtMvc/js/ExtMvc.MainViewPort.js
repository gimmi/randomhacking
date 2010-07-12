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

		function _setTabIdentifier(tab, id) {
			tab.tabContentIdentifier = id;
		}

		function _getTabIdentifier(tab) {
			return tab.tabContentIdentifier;
		}

		function _createTab(title, newItem, id) {
			var tab, ret = false;

			Ext.each(_tabPanel.items.items, function (item) {
				if (_getTabIdentifier(item) === id) {
					tab = item;
					return false;
				}
			});

			if (!tab) {
				tab = _tabPanel.add(new Ext.Panel({
					layout: 'fit',
					items: newItem,
					title: title,
					closable: true
				}));
				_setTabIdentifier(tab, id);
				ret = true;
			}

			tab.show();
			return ret;
		}

				function _onNsCategoryNewItem (sender) {
					var control = new ExtMvc.Ns.CategoryEditPanel();
					_createTab('New Category', control, 'NsCategory-new');
				}
				function _onNsCategoryEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.Ns.CategoryEditPanel();
					description = ExtMvc.Ns.Category.toString(item);
					if (_createTab('Category ' + description, control, 'NsCategory-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onNsCategoryNormalClick (sender, item) {
							var control = new ExtMvc.Ns.CategoryNormalSearchPanel();
							if (_createTab('Search Category Normal', control, 'NsCategory-search-Normal')) {
								control.on('edititem', _onNsCategoryEditItem);
								control.on('newitem', _onNsCategoryNewItem);
							}
						}
						
						function _onNsCustomerNewItem (sender) {
					var control = new ExtMvc.Ns.CustomerEditPanel();
					_createTab('New Customer', control, 'NsCustomer-new');
				}
				function _onNsCustomerEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.Ns.CustomerEditPanel();
					description = ExtMvc.Ns.Customer.toString(item);
					if (_createTab('Customer ' + description, control, 'NsCustomer-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onNsCustomerNormalClick (sender, item) {
							var control = new ExtMvc.Ns.CustomerNormalSearchPanel();
							if (_createTab('Search Customer Normal', control, 'NsCustomer-search-Normal')) {
								control.on('edititem', _onNsCustomerEditItem);
								control.on('newitem', _onNsCustomerNewItem);
							}
						}
						
						function _onNsEmployeeNewItem (sender) {
					var control = new ExtMvc.Ns.EmployeeEditPanel();
					_createTab('New Employee', control, 'NsEmployee-new');
				}
				function _onNsEmployeeEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.Ns.EmployeeEditPanel();
					description = ExtMvc.Ns.Employee.toString(item);
					if (_createTab('Employee ' + description, control, 'NsEmployee-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onNsEmployeeNormalClick (sender, item) {
							var control = new ExtMvc.Ns.EmployeeNormalSearchPanel();
							if (_createTab('Search Employee Normal', control, 'NsEmployee-search-Normal')) {
								control.on('edititem', _onNsEmployeeEditItem);
								control.on('newitem', _onNsEmployeeNewItem);
							}
						}
						
						function _onOrderDetailNewItem (sender) {
					var control = new ExtMvc.OrderDetailEditPanel();
					_createTab('New OrderDetail', control, 'OrderDetail-new');
				}
				function _onOrderDetailEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.OrderDetailEditPanel();
					description = ExtMvc.OrderDetail.toString(item);
					if (_createTab('OrderDetail ' + description, control, 'OrderDetail-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onOrderDetailClick (sender, item) {
							var control = new ExtMvc.OrderDetailSearchPanel();
							if (_createTab('Search OrderDetail ', control, 'OrderDetail-search-')) {
								control.on('edititem', _onOrderDetailEditItem);
								control.on('newitem', _onOrderDetailNewItem);
							}
						}
						
						function _onOrderNewItem (sender) {
					var control = new ExtMvc.OrderEditPanel();
					_createTab('New Order', control, 'Order-new');
				}
				function _onOrderEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.OrderEditPanel();
					description = ExtMvc.Order.toString(item);
					if (_createTab('Order ' + description, control, 'Order-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onOrderNormalClick (sender, item) {
							var control = new ExtMvc.OrderNormalSearchPanel();
							if (_createTab('Search Order Normal', control, 'Order-search-Normal')) {
								control.on('edititem', _onOrderEditItem);
								control.on('newitem', _onOrderNewItem);
							}
						}
						
						function _onProductNewItem (sender) {
					var control = new ExtMvc.ProductEditPanel();
					_createTab('New Product', control, 'Product-new');
				}
				function _onProductEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.ProductEditPanel();
					description = ExtMvc.Product.toString(item);
					if (_createTab('Product ' + description, control, 'Product-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onProductNormalClick (sender, item) {
							var control = new ExtMvc.ProductNormalSearchPanel();
							if (_createTab('Search Product Normal', control, 'Product-search-Normal')) {
								control.on('edititem', _onProductEditItem);
								control.on('newitem', _onProductNewItem);
							}
						}
						
						function _onRegionNewItem (sender) {
					var control = new ExtMvc.RegionEditPanel();
					_createTab('New Region', control, 'Region-new');
				}
				function _onRegionEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.RegionEditPanel();
					description = ExtMvc.Region.toString(item);
					if (_createTab('Region ' + description, control, 'Region-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onRegionNormalClick (sender, item) {
							var control = new ExtMvc.RegionNormalSearchPanel();
							if (_createTab('Search Region Normal', control, 'Region-search-Normal')) {
								control.on('edititem', _onRegionEditItem);
								control.on('newitem', _onRegionNewItem);
							}
						}
						
						function _onShipperNewItem (sender) {
					var control = new ExtMvc.ShipperEditPanel();
					_createTab('New Shipper', control, 'Shipper-new');
				}
				function _onShipperEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.ShipperEditPanel();
					description = ExtMvc.Shipper.toString(item);
					if (_createTab('Shipper ' + description, control, 'Shipper-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onShipperNormalClick (sender, item) {
							var control = new ExtMvc.ShipperNormalSearchPanel();
							if (_createTab('Search Shipper Normal', control, 'Shipper-search-Normal')) {
								control.on('edititem', _onShipperEditItem);
								control.on('newitem', _onShipperNewItem);
							}
						}
						
						function _onSupplierNewItem (sender) {
					var control = new ExtMvc.SupplierEditPanel();
					_createTab('New Supplier', control, 'Supplier-new');
				}
				function _onSupplierEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.SupplierEditPanel();
					description = ExtMvc.Supplier.toString(item);
					if (_createTab('Supplier ' + description, control, 'Supplier-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onSupplierNormalClick (sender, item) {
							var control = new ExtMvc.SupplierNormalSearchPanel();
							if (_createTab('Search Supplier Normal', control, 'Supplier-search-Normal')) {
								control.on('edititem', _onSupplierEditItem);
								control.on('newitem', _onSupplierNewItem);
							}
						}
						
						function _onTerritoryNewItem (sender) {
					var control = new ExtMvc.TerritoryEditPanel();
					_createTab('New Territory', control, 'Territory-new');
				}
				function _onTerritoryEditItem (sender, item) {
					var editTab, description, control;
					control = new ExtMvc.TerritoryEditPanel();
					description = ExtMvc.Territory.toString(item);
					if (_createTab('Territory ' + description, control, 'Territory-' + item.StringId)) {
						control.loadItem(item.StringId);
					}
				}
						function _onTerritoryNormalClick (sender, item) {
							var control = new ExtMvc.TerritoryNormalSearchPanel();
							if (_createTab('Search Territory Normal', control, 'Territory-search-Normal')) {
								control.on('edititem', _onTerritoryEditItem);
								control.on('newitem', _onTerritoryNewItem);
							}
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
							click: _onNsCategoryNormalClick
						}
					}, {
						text: 'Create Category',
						leaf: true,
						listeners: {
							click: _onNsCategoryNewItem
						}
					}]
				}, {
					text: 'Customer',
					children: [{
						text: 'Search Customer Normal',
						leaf: true,
						listeners: {
							click: _onNsCustomerNormalClick
						}
					}, {
						text: 'Create Customer',
						leaf: true,
						listeners: {
							click: _onNsCustomerNewItem
						}
					}]
				}, {
					text: 'Employee',
					children: [{
						text: 'Search Employee Normal',
						leaf: true,
						listeners: {
							click: _onNsEmployeeNormalClick
						}
					}, {
						text: 'Create Employee',
						leaf: true,
						listeners: {
							click: _onNsEmployeeNewItem
						}
					}]
				}, {
					text: 'OrderDetail',
					children: [{
						text: 'Search OrderDetail ',
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