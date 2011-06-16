Ext.define('Spike.view.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	initComponent: function () {
		this.items = [{
			title: 'Hello Ext',
			html: 'Hello! Welcome to Ext JS.'
		}];

		this.callParent(arguments);
	}
});
