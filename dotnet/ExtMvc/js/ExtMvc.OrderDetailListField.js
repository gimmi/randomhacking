/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.OrderDetailListField = Ext.extend(Ext.form.Field, {
	initComponent: function () {
		var cfg = Ext.copyTo({
			id: this.id + '-gridpanel',
			store: new Ext.data.Store({
				proxy: new Ext.data.MemoryProxy({ items: [] }),
				reader: new ExtMvc.OrderDetailJsonReader()
			})
		}, this.initialConfig, []);
		this.gridPanel = new ExtMvc.OrderDetailGridPanel(cfg);
		ExtMvc.OrderDetailListField.superclass.initComponent.call(this);
	},

	onRender: function (ct, position) {
		// TODO This creates a hidden field above the grid. Check if this is good or not
		this.autoCreate = {
			id: this.id,
			name: this.name,
			type: 'hidden',
			tag: 'input'
		};
		ExtMvc.OrderDetailListField.superclass.onRender.call(this, ct, position);
		this.wrap = this.el.wrap({ cls: 'x-form-field-wrap' });
		this.resizeEl = this.positionEl = this.wrap;
		this.gridPanel.render(this.wrap);
	},

	onResize: function (w, h, aw, ah) {
		ExtMvc.OrderDetailListField.superclass.onResize.call(this, w, h, aw, ah);
		this.gridPanel.setSize(w, h);
	},

	onEnable: function () {
		ExtMvc.OrderDetailListField.superclass.onEnable.call(this);
		this.gridPanel.enable();
	},

	onDisable: function () {
		ExtMvc.OrderDetailListField.superclass.onDisable.call(this);
		this.gridPanel.disable();
	},

	beforeDestroy: function () {
		Ext.destroy(this.gridPanel);
		ExtMvc.OrderDetailListField.superclass.beforeDestroy.call(this);
	},

	setValue: function (v) {
		this.gridPanel.getStore().proxy.data.items = v;
		this.gridPanel.getStore().load();
		return ExtMvc.OrderDetailListField.superclass.setValue.call(this, v);
	},

	getValue: function () {
		return this.gridPanel.getStore().proxy.data.items;
	}
});

Ext.reg('ExtMvc.OrderDetailListField', ExtMvc.OrderDetailListField);