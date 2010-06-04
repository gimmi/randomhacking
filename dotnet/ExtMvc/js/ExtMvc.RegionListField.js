/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.RegionListField = Ext.extend(Ext.form.Field, {
	initComponent: function () {
		var cfg = Ext.copyTo({
			id: this.id + '-gridpanel',
			store: new Ext.data.Store({
				autoDestroy: true,
				proxy: new Ext.data.MemoryProxy({ items: [] }),
				reader: new ExtMvc.RegionJsonReader()
			})
		}, this.initialConfig, []);
		this.gridPanel = new ExtMvc.RegionGridPanel(cfg);
		ExtMvc.RegionListField.superclass.initComponent.call(this);
	},

	onRender: function (ct, position) {
		// TODO This creates a hidden field above the grid. Check if this is good or not
		this.autoCreate = {
			id: this.id,
			name: this.name,
			type: 'hidden',
			tag: 'input'
		};
		ExtMvc.RegionListField.superclass.onRender.call(this, ct, position);
		this.wrap = this.el.wrap({ cls: 'x-form-field-wrap' });
		this.resizeEl = this.positionEl = this.wrap;
		this.gridPanel.render(this.wrap);
	},

	onResize: function (w, h, aw, ah) {
		ExtMvc.RegionListField.superclass.onResize.call(this, w, h, aw, ah);
		this.gridPanel.setSize(w, h);
	},

	onEnable: function () {
		ExtMvc.RegionListField.superclass.onEnable.call(this);
		this.gridPanel.enable();
	},

	onDisable: function () {
		ExtMvc.RegionListField.superclass.onDisable.call(this);
		this.gridPanel.disable();
	},

	beforeDestroy: function () {
		Ext.destroy(this.gridPanel);
		ExtMvc.RegionListField.superclass.beforeDestroy.call(this);
	},

	setValue: function (v) {
		this.gridPanel.getStore().proxy.data.items = v;
		this.gridPanel.getStore().load();
		return ExtMvc.RegionListField.superclass.setValue.call(this, v);
	},

	getValue: function () {
		return this.gridPanel.getStore().proxy.data.items;
	}
});

Ext.reg('ExtMvc.RegionListField', ExtMvc.RegionListField);