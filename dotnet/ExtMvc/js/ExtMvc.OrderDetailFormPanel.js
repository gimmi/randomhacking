/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.OrderDetailFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this,
		_saveItemButtonHandler = function () {
			_this.el.mask('Saving...', 'x-mask-loading');
			Rpc.call({
				url: 'OrderDetail/Save',
				params: { item: _this.getForm().getFieldValues() },
				success: function (result) {
					_this.el.unmask();
					if (result.success) {
						Ext.MessageBox.show({ msg: 'Changes saved successfully.', icon: Ext.MessageBox.INFO, buttons: Ext.MessageBox.OK });
					} else {
						_this.getForm().markInvalid(result.errors.item);
						Ext.MessageBox.show({ msg: 'Error saving data. Correct errors and retry.', icon: Ext.MessageBox.ERROR, buttons: Ext.MessageBox.OK });
					}
				}
			});
		},
		_refreshItemButtonHandler = function () {
			Ext.MessageBox.confirm('Refresh', 'All modifications will be lost, continue?', function (buttonId) {
				if (buttonId === 'yes') {
					var stringId = _this.getForm().getFieldValues().StringId;
					if (Ext.isEmpty(stringId)) {
						_this.getForm().reset();
					} else {
						_this.loadItem(stringId);
					}
				}
			});
		};

		Ext.apply(_this, {
			border: false,
			layout: 'fit',
			items: new ExtMvc.OrderDetailContainer(),

			tbar: [
				{ text: 'Save', handler: _saveItemButtonHandler, icon: 'images/disk.png', cls: 'x-btn-text-icon' },
				{ text: 'Refresh', handler: _refreshItemButtonHandler, icon: 'images/arrow_refresh.png', cls: 'x-btn-text-icon' }
			],
			loadItem: function (stringId) {
				_this.el.mask('Loading...', 'x-mask-loading');
				Rpc.call({
					url: 'OrderDetail/Load',
					params: { stringId: stringId },
					success: function (item) {
						_this.el.unmask();
						_this.getForm().setValues(item);
					}
				});
			}

		});

		ExtMvc.OrderDetailFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});