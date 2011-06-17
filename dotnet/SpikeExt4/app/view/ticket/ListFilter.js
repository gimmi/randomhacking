Ext.define('Spike.view.ticket.ListFilter', {
	extend: 'Ext.panel.Panel',
	alias: 'widget.ticketlistfilter',

	layout: 'border',

	initComponent: function () {
		this.dockedItems = [{
			xtype: 'toolbar',
			dock: 'top',
			items: [
				{ xtype: 'button', text: 'Button 1' }
			]
		}];
		this.items = [{
			region: 'north',
			xtype: 'ticketfilter',
			height: 200,
			collapsible: true
		}, {
			region: 'center',
			xtype: 'ticketlist',
			border: false
		}];

		this.callParent(arguments);
	}
});