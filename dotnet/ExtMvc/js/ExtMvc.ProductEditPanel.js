/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.ProductEditPanel = Ext.extend(Ext.Panel, {
	initComponent: function () {
		var _this = this,
		_formPanel = new ExtMvc.ProductFormPanel(),
		_saveItemButtonHandler = function () {
			_this.el.mask('Saving...', 'x-mask-loading');
			Rpc.call({
				url: 'Product/Save',
				params: { item: _formPanel.getForm().getFieldValues() },
				success: function (result) {
					_this.el.unmask();
					if (result.success) {
						Ext.MessageBox.show({ msg: 'Changes saved successfully.', icon: Ext.MessageBox.INFO, buttons: Ext.MessageBox.OK });
					} else {
						_formPanel.getForm().markInvalid(result.errors.item);
						Ext.MessageBox.show({ msg: 'Error saving data. Correct errors and retry.', icon: Ext.MessageBox.ERROR, buttons: Ext.MessageBox.OK });
					}
				}
			});
		},
		_refreshItemButtonHandler = function () {
			Ext.MessageBox.confirm('Refresh', 'All modifications will be lost, continue?', function (buttonId) {
				if (buttonId === 'yes') {
					var stringId = _formPanel.getForm().getFieldValues().StringId;
					if (Ext.isEmpty(stringId)) {
						_formPanel.getForm().reset();
					} else {
						_this.loadItem(stringId);
					}
				}
			});
		};

		Ext.apply(_this, {
			border: false,
			layout: 'fit',
			items: _formPanel,
			tbar: [
				{ text: 'Save', handler: _saveItemButtonHandler, icon: 'images/disk.png', cls: 'x-btn-text-icon' },
				{ text: 'Refresh', handler: _refreshItemButtonHandler, icon: 'images/arrow_refresh.png', cls: 'x-btn-text-icon' }
			],
			loadItem: function (stringId) {
				_this.el.mask('Loading...', 'x-mask-loading');
				Rpc.call({
					url: 'Product/Load',
					params: { stringId: stringId },
					success: function (item) {
						_this.el.unmask();
						_formPanel.getForm().setValues(item);
					}
				});
			}
		});

		ExtMvc.ProductEditPanel.superclass.initComponent.apply(_this, arguments);
	}
});