Ext.define('Spike.view.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	initComponent: function() {
		this.items = [{
			xtype: 'panel',
			layout: { type: 'vbox', align: 'stretch' },
			items: [{
				flex: 1,
				xtype: 'panel',
				title: 'Tickets',
				layout: 'fit',
				items: {
					xtype: 'ticketlist',
					border: 0
				}
			}, {
				flex: 1,
				xtype: 'panel',
				title: 'Other'
			}]
		}];

		this.callParent(arguments);
	}
});