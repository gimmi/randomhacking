Ext.define('Spike.view.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	initComponent: function () {
		this.items = [ {
			xtype: 'ticketlistfilter'
		} ];

		this.callParent(arguments);
	}
});
