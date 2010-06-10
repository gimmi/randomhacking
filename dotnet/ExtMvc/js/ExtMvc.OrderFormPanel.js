/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.OrderFormPanel = Ext.extend(Ext.form.FormPanel, {
	border: false,
	layout: 'vbox',
	layoutConfig: {
		align: 'stretch',
		pack: 'start'
	},

	initComponent: function () {
		// see http://www.extjs.com/forum/showthread.php?98131
		this.items = [{
			layout: 'form',
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'OrderId', fieldLabel: 'OrderId', xtype: 'numberfield' },
				{ name: 'OrderDate', fieldLabel: 'OrderDate', xtype: 'datefield' },
				{ name: 'RequiredDate', fieldLabel: 'RequiredDate', xtype: 'datefield' },
				{ name: 'ShippedDate', fieldLabel: 'ShippedDate', xtype: 'datefield' },
				{ name: 'Freight', fieldLabel: 'Freight', xtype: 'numberfield' },
				{ name: 'ShipName', fieldLabel: 'ShipName', xtype: 'textfield' },
				{ name: 'ShipAddress', fieldLabel: 'ShipAddress', xtype: 'textfield' },
				{ name: 'ShipCity', fieldLabel: 'ShipCity', xtype: 'textfield' },
				{ name: 'ShipRegion', fieldLabel: 'ShipRegion', xtype: 'textfield' },
				{ name: 'ShipPostalCode', fieldLabel: 'ShipPostalCode', xtype: 'textfield' },
				{ name: 'ShipCountry', fieldLabel: 'ShipCountry', xtype: 'textfield' },
				{ name: 'Customer', fieldLabel: 'Customer', xtype: 'ExtMvc.CustomerField' },
				{ name: 'Employee', fieldLabel: 'Employee', xtype: 'ExtMvc.EmployeeField' }
			]
		}];

		this.tbar = {
			xtype: 'toolbar',
			items: [{
				xtype: 'button',
				text: 'Save',
				icon: '/images/save.png',
				cls: 'x-btn-text-icon',
				handler: this.saveItemButtonHandler,
				scope: this
			}, {
				xtype: 'button',
				text: 'Refresh',
				icon: '/ext/resources/images/default/grid/refresh.gif',
				cls: 'x-btn-text-icon',
				handler: this.refreshItemButtonHandler,
				scope: this
			}, {
				xtype: 'button',
				text: 'Delete',
				icon: '/images/delete.png',
				cls: 'x-btn-text-icon',
				handler: this.deleteItemButtonHandler,
				scope: this
			}]
		};

		ExtMvc.OrderFormPanel.superclass.initComponent.call(this);
	},

	loadItem: function (stringId) {
		this.el.mask('Loading...', 'x-mask-loading');
		Rpc.call({
			url: '/Order/Load',
			params: { stringId: stringId },
			scope: this,
			success: function (item) {
				this.el.unmask();
				this.setItem(item);
			}
		});
	},

	setItem: function (item) {
		this.getForm().setValues(item);
	},

	saveItemButtonHandler: function () {
		this.el.mask('Saving...', 'x-mask-loading');
		Rpc.call({
			url: '/Order/Save',
			params: { item: this.getForm().getFieldValues() },
			scope: this,
			success: function (result) {
				this.el.unmask();
				if (result.success) {
					Ext.MessageBox.show({ msg: 'Changes saved successfully.', icon: Ext.MessageBox.INFO, buttons: Ext.MessageBox.OK });
				} else {
					this.getForm().markInvalid(result.errors.item);
					Ext.MessageBox.show({ msg: 'Error saving data. Correct errors and retry.', icon: Ext.MessageBox.ERROR, buttons: Ext.MessageBox.OK });
				}
			}
		});
	},

	refreshItemButtonHandler: function () {
		Ext.MessageBox.confirm('Refresh', 'All modifications will be lost, continue?', function (buttonId) {
			if (buttonId === 'yes') {
				var stringId = this.getForm().getFieldValues().StringId;
				if (Ext.isEmpty(stringId)) {
					this.getForm().reset();
				} else {
					this.loadItem(stringId);
				}
			}
		}, this);
	},

	deleteItemButtonHandler: function () {
		Ext.MessageBox.confirm('Delete', 'Are you sure?', function (buttonId) {
			if (buttonId === 'yes') {
				Rpc.call({
					url: '/$name$/Delete',
					params: { item: this.getForm().getFieldValues() },
					scope: this,
					success: function () {
						this.el.mask('Item deleted');
					}
				});
			}
		}, this);
	}
});