/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.TerritoryField = Ext.extend(Ext.form.TriggerField, {
	editable: false,
	hideTrigger: true,
	onTriggerClick: function () {
		this.getWindow().show(this.getEl());
	},

	getWindow: function () {
		this.window = this.window || new Ext.Window({
			title: 'Search Territory',
			width: 600,
			height: 300,
			layout: 'fit',
			maximizable: true,
			closeAction: 'hide',
			items: new ExtMvc.TerritoryNormalSearchPanel({
				listeners: {
					itemselected: this.searchPanel_itemSelected,
					scope: this
				}
			}),
			buttons: [
				{ text: 'Select None', handler: this.selectNoneButton_click, scope: this },
				{ text: 'Select', handler: this.selectButton_click, scope: this },
				{ text: 'Cancel', handler: this.cancelButton_click, scope: this }
			]
		});
		return this.window;
	},

	getSearchPanel: function () {
		return this.getWindow().get(0);
	},

	searchPanel_itemSelected: function (sender, item) {
		this.setValue(item);
		this.getWindow().hide();
	},

	selectNoneButton_click: function (button, event) {
		this.setValue(null);
		this.getWindow().hide();
	},

	selectButton_click: function (button, event) {
		var selectedItem = this.getSearchPanel().getSelectedItem();
		this.setValue(selectedItem);
		this.getWindow().hide();
	},

	cancelButton_click: function (button, event) {
		this.getWindow().hide();
	},

	setValue: function (v) {
		this.selectedItem = v;
		return ExtMvc.TerritoryField.superclass.setValue.call(this, ExtMvc.Territory.toString(v));
	},

	getValue: function () {
		return this.selectedItem;
	}
});

Ext.reg('ExtMvc.TerritoryField', ExtMvc.TerritoryField);