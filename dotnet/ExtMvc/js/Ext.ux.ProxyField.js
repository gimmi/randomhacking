/*jslint white: true, browser: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext */
"use strict";

Ext.ux.ProxyField = Ext.extend(Ext.form.Field, {
	onRender: function () {
		// TODO This creates a hidden field above the grid. Check if this is good or not
		this.autoCreate = {
			id: this.id,
			name: this.name,
			type: 'hidden',
			tag: 'input'
		};
		Ext.ux.ProxyField.superclass.onRender.apply(this, arguments);
		this.wrap = this.el.wrap({ cls: 'x-form-field-wrap' });
		this.resizeEl = this.positionEl = this.wrap;
		this.item.render(this.wrap);
	},
	onResize: function (w, h) {
		Ext.ux.ProxyField.superclass.onResize.apply(this, arguments);
		this.item.setSize(w, h);
	},
	onEnable: function () {
		Ext.ux.ProxyField.superclass.onEnable.apply(this, arguments);
		this.item.enable();
	},
	onDisable: function () {
		Ext.ux.ProxyField.superclass.onDisable.apply(this, arguments);
		this.item.disable();
	},
	beforeDestroy: function () {
		Ext.destroy(this.item);
		Ext.ux.ProxyField.superclass.beforeDestroy.apply(this, arguments);
	}
});
