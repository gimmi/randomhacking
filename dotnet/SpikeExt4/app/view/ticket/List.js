Ext.define('Spike.view.ticket.List', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ticketlist',
		
	store: 'Tickets',

	title: 'All Tickets',

	initComponent: function () {
		this.columns = [
            { header: 'Title', dataIndex: 'title', flex: 1 },
            { header: 'Description', dataIndex: 'description', flex: 1 },
            { header: 'State', dataIndex: 'state', flex: 1 }
        ];

		this.callParent(arguments);
	}
});