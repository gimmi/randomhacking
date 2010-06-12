/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.CategoryListField = Ext.extend(Ext.form.Field, {
	initComponent: function () {
		var cfg = Ext.copyTo({
			id: this.id + '-gridpanel',
			store: new Ext.data.Store({
				autoDestroy: true,
				proxy: new Ext.data.MemoryProxy({ items: [] }),
				reader: new ExtMvc.CategoryJsonReader()
			})
		}, this.initialConfig, []);
		this.gridPanel = new ExtMvc.CategoryGridPanel(cfg);
		ExtMvc.CategoryListField.superclass.initComponent.apply(this, arguments);
	},

	onRender: function (ct, position) {
		// TODO This creates a hidden field above the grid. Check if this is good or not
		this.autoCreate = {
			id: this.id,
			name: this.name,
			type: 'hidden',
			tag: 'input'
		};
		ExtMvc.CategoryListField.superclass.onRender.call(this, ct, position);
		this.wrap = this.el.wrap({ cls: 'x-form-field-wrap' });
		this.resizeEl = this.positionEl = this.wrap;
		this.gridPanel.render(this.wrap);
	},

	onResize: function (w, h, aw, ah) {
		ExtMvc.CategoryListField.superclass.onResize.apply(this, arguments);
		this.gridPanel.setSize(w, h);
	},

	onEnable: function () {
		ExtMvc.CategoryListField.superclass.onEnable.apply(this, arguments);
		this.gridPanel.enable();
	},

	onDisable: function () {
		ExtMvc.CategoryListField.superclass.onDisable.apply(this, arguments);
		this.gridPanel.disable();
	},

	beforeDestroy: function () {
		Ext.destroy(this.gridPanel);
		ExtMvc.CategoryListField.superclass.beforeDestroy.apply(this, arguments);
	},

	setValue: function (v) {
		this.gridPanel.getStore().proxy.data.items = v;
		this.gridPanel.getStore().load();
		return ExtMvc.CategoryListField.superclass.setValue.apply(this, arguments);
	},

	getValue: function () {
		return this.gridPanel.getStore().proxy.data.items;
	}
});

Ext.reg('ExtMvc.CategoryListField', ExtMvc.CategoryListField);