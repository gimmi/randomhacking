/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.TerritoryField = Ext.extend(Ext.form.TriggerField, {
	editable: false,
	hideTrigger: true,
	onTriggerClick: function () {
		var searchPanel = new ExtMvc.TerritoryNormalSearchContainer();
		this.window = new Ext.Window({
			modal: true,
			title: 'Search Territory',
			width: 600,
			height: 300,
			layout: 'fit',
			items: searchPanel
		});
		searchPanel.on('itemselected', this.searchPanel_itemSelected, this);
		this.window.show();
	},

	searchPanel_itemSelected: function (sender, item) {
		this.setValue(item.StringId);
		this.window.close();
	}
});

Ext.reg('ExtMvc.TerritoryField', ExtMvc.TerritoryField);