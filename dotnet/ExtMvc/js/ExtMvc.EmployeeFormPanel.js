/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.EmployeeFormPanel = Ext.extend(Ext.form.FormPanel, {
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
				{ name: 'EmployeeId', fieldLabel: 'EmployeeId', xtype: 'numberfield' },
				{ name: 'LastName', fieldLabel: 'LastName', xtype: 'textfield' },
				{ name: 'FirstName', fieldLabel: 'FirstName', xtype: 'textfield' },
				{ name: 'Title', fieldLabel: 'Title', xtype: 'textfield' },
				{ name: 'TitleOfCourtesy', fieldLabel: 'TitleOfCourtesy', xtype: 'textfield' },
				{ name: 'BirthDate', fieldLabel: 'BirthDate', xtype: 'datefield' },
				{ name: 'HireDate', fieldLabel: 'HireDate', xtype: 'datefield' },
				{ name: 'Address', fieldLabel: 'Address', xtype: 'textfield' },
				{ name: 'City', fieldLabel: 'City', xtype: 'textfield' },
				{ name: 'Region', fieldLabel: 'Region', xtype: 'textfield' },
				{ name: 'PostalCode', fieldLabel: 'PostalCode', xtype: 'textfield' },
				{ name: 'Country', fieldLabel: 'Country', xtype: 'textfield' },
				{ name: 'HomePhone', fieldLabel: 'HomePhone', xtype: 'textfield' },
				{ name: 'Extension', fieldLabel: 'Extension', xtype: 'textfield' },
				{ name: 'Notes', fieldLabel: 'Notes', xtype: 'textfield' },
				{ name: 'PhotoPath', fieldLabel: 'PhotoPath', xtype: 'textfield' }
			]
		}];

		this.tbar = [
			{ text: 'Save', handler: this.saveItemButtonHandler, icon: '/images/disk.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'Refresh', handler: this.refreshItemButtonHandler, icon: '/images/arrow_refresh.png', cls: 'x-btn-text-icon', scope: this }
		];

		ExtMvc.EmployeeFormPanel.superclass.initComponent.apply(this, arguments);
	},

	loadItem: function (stringId) {
		this.el.mask('Loading...', 'x-mask-loading');
		Rpc.call({
			url: '/Employee/Load',
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
			url: '/Employee/Save',
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