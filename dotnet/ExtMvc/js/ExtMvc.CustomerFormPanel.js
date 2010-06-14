/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.CustomerFormPanel = Ext.extend(Ext.form.FormPanel, {
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
				{ name: 'CustomerId', fieldLabel: 'CustomerId', xtype: 'textfield' },
				{ name: 'CompanyName', fieldLabel: 'CompanyName', xtype: 'textfield' },
				{ name: 'ContactName', fieldLabel: 'ContactName', xtype: 'textfield' },
				{ name: 'ContactTitle', fieldLabel: 'ContactTitle', xtype: 'textfield' },
				{ name: 'Address', fieldLabel: 'Address', xtype: 'textfield' },
				{ name: 'City', fieldLabel: 'City', xtype: 'textfield' },
				{ name: 'Region', fieldLabel: 'Region', xtype: 'textfield' },
				{ name: 'PostalCode', fieldLabel: 'PostalCode', xtype: 'textfield' },
				{ name: 'Country', fieldLabel: 'Country', xtype: 'textfield' },
				{ name: 'Phone', fieldLabel: 'Phone', xtype: 'textfield' },
				{ name: 'Fax', fieldLabel: 'Fax', xtype: 'textfield' }
			]
		}, {
			flex: 1,
			xtype: 'tabpanel',
			plain: true,
			border: false,
			activeTab: 0,
			deferredRender: false, // IMPORTANT! See http://www.extjs.com/deploy/dev/examples/form/dynamic.js
			items: [
				{ name: 'Customerdemographics', title: 'Customerdemographics', xtype: 'ExtMvc.CustomerDemographicListField' }
			]
		}];

		this.tbar = [
			{ text: 'Save', handler: this.saveItemButtonHandler, icon: 'images/disk.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'Refresh', handler: this.refreshItemButtonHandler, icon: 'images/arrow_refresh.png', cls: 'x-btn-text-icon', scope: this }
		];

		ExtMvc.CustomerFormPanel.superclass.initComponent.apply(this, arguments);
	},

	loadItem: function (stringId) {
		this.el.mask('Loading...', 'x-mask-loading');
		Rpc.call({
			url: 'Customer/Load',
			params: { stringId: stringId },
			scope: this,
			success: function (item) {
				this.el.unmask();
				this.getForm().setValues(item);
			}
		});
	},

	saveItemButtonHandler: function () {
		this.el.mask('Saving...', 'x-mask-loading');
		Rpc.call({
			url: 'Customer/Save',
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
	}
});