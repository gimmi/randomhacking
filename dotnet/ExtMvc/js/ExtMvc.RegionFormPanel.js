/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.RegionFormPanel = Ext.extend(Ext.form.FormPanel, {
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
				{ name: 'RegionId', fieldLabel: 'RegionId', xtype: 'numberfield' },
				{ name: 'RegionDescription', fieldLabel: 'RegionDescription', xtype: 'textfield' }
			]
		}];

		this.tbar = [
			{ text: 'Save', handler: this.saveItemButtonHandler, icon: '/images/disk.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'Refresh', handler: this.refreshItemButtonHandler, icon: '/images/arrow_refresh.png', cls: 'x-btn-text-icon', scope: this },
			{ text: 'Delete', handler: this.deleteItemButtonHandler, icon: '/images/delete.png', cls: 'x-btn-text-icon', scope: this }
		];

		ExtMvc.RegionFormPanel.superclass.initComponent.apply(this, arguments);
	},

	loadItem: function (stringId) {
		this.el.mask('Loading...', 'x-mask-loading');
		Rpc.call({
			url: '/Region/Load',
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
			url: '/Region/Save',
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
		var stringId = this.getForm().getFieldValues().StringId;
		if (!Ext.isEmpty(stringId)) {
			Ext.MessageBox.confirm('Delete', 'Are you sure?', function (buttonId) {
				if (buttonId === 'yes') {
					Rpc.call({
						url: '/Region/Delete',
						params: { stringId: stringId },
						scope: this,
						success: function (result) {
							this.el.mask('Item deleted');
						}
					});
				}
			}, this);
		}
	}
});