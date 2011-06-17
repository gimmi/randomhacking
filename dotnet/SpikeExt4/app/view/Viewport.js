Ext.define('Spike.view.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	initComponent: function () {
		this.items = [{
			xtype: 'ticketlist'
		}];

		this.callParent(arguments);
	}
});
