/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.MainViewport = Ext.extend(Ext.Viewport, {
	layout: 'border',
	initComponent: function () {

		this.treePanel = new Ext.tree.TreePanel({
			xtype: 'treepanel',
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
							click: function () {
								var searchTab = this.openTab('Search Category Normal', ExtMvc.CategoryNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Category.toString(item);
									editTab = this.openTab('Category ' + description, ExtMvc.CategoryFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Category',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Category', ExtMvc.CategoryFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'CustomerDemographic',
					children: [{
						text: 'Search CustomerDemographic Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search CustomerDemographic Normal', ExtMvc.CustomerDemographicNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.CustomerDemographic.toString(item);
									editTab = this.openTab('CustomerDemographic ' + description, ExtMvc.CustomerDemographicFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create CustomerDemographic',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New CustomerDemographic', ExtMvc.CustomerDemographicFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'Customer',
					children: [{
						text: 'Search Customer Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search Customer Normal', ExtMvc.CustomerNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Customer.toString(item);
									editTab = this.openTab('Customer ' + description, ExtMvc.CustomerFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Customer',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Customer', ExtMvc.CustomerFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'Employee',
					children: [{
						text: 'Search Employee Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search Employee Normal', ExtMvc.EmployeeNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Employee.toString(item);
									editTab = this.openTab('Employee ' + description, ExtMvc.EmployeeFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Employee',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Employee', ExtMvc.EmployeeFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'OrderDetail',
					children: [{
						text: 'Search OrderDetail',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search OrderDetail', ExtMvc.OrderDetailSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.OrderDetail.toString(item);
									editTab = this.openTab('OrderDetail ' + description, ExtMvc.OrderDetailFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create OrderDetail',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New OrderDetail', ExtMvc.OrderDetailFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'Order',
					children: [{
						text: 'Search Order Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search Order Normal', ExtMvc.OrderNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Order.toString(item);
									editTab = this.openTab('Order ' + description, ExtMvc.OrderFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Order',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Order', ExtMvc.OrderFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'Product',
					children: [{
						text: 'Search Product Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search Product Normal', ExtMvc.ProductNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Product.toString(item);
									editTab = this.openTab('Product ' + description, ExtMvc.ProductFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Product',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Product', ExtMvc.ProductFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'Region',
					children: [{
						text: 'Search Region Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search Region Normal', ExtMvc.RegionNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Region.toString(item);
									editTab = this.openTab('Region ' + description, ExtMvc.RegionFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Region',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Region', ExtMvc.RegionFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'Shipper',
					children: [{
						text: 'Search Shipper Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search Shipper Normal', ExtMvc.ShipperNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Shipper.toString(item);
									editTab = this.openTab('Shipper ' + description, ExtMvc.ShipperFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Shipper',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Shipper', ExtMvc.ShipperFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'Supplier',
					children: [{
						text: 'Search Supplier Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search Supplier Normal', ExtMvc.SupplierNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Supplier.toString(item);
									editTab = this.openTab('Supplier ' + description, ExtMvc.SupplierFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Supplier',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Supplier', ExtMvc.SupplierFormPanel);
							},
							scope: this
						}
					}]
				}, {
					text: 'Territory',
					children: [{
						text: 'Search Territory Normal',
						leaf: true,
						listeners: {
							click: function () {
								var searchTab = this.openTab('Search Territory Normal', ExtMvc.TerritoryNormalSearchContainer);
								searchTab.on('itemselected', function(sender, item) {
									var editTab, description;
									description = ExtMvc.Territory.toString(item);
									editTab = this.openTab('Territory ' + description, ExtMvc.TerritoryFormPanel);
									editTab.loadItem(item.StringId);
								}, this);
							},
							scope: this
						}
					}, {
						text: 'Create Territory',
						leaf: true,
						listeners: {
							click: function () {
								this.openTab('New Territory', ExtMvc.TerritoryFormPanel);
							},
							scope: this
						}
					}]
				}]
			},
			loader: {}
		});

		this.tabPanel = new Ext.TabPanel({
			xtype: 'tabpanel',
			activeTab: 0,
			region: 'center',
			items: [{
				xtype: 'panel',
				title: 'Welcome Page'
			}]
		});

		this.items = [this.treePanel, this.tabPanel];
		ExtMvc.MainViewport.superclass.initComponent.call(this);
	},
	
	openTab: function (title, Constructor) {
		var tab;

		Ext.each(this.tabPanel.items.items, function (item) {
			if (item.title === title) {
				tab = item;
				return false;
			}
		});

		tab = tab || this.tabPanel.add(new Constructor({
			title: title,
			closable: true
		}));

		tab.show();
		return tab;
	}
});