Ext.define('Spike.view.ticket.Filter', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ticketfilter',
		
	store: 'Tickets',

	title: 'Ticket Filters',

	initComponent: function () {
		this.columns = [
            { header: 'Title', dataIndex: 'title', flex: 1 },
            { header: 'Description', dataIndex: 'description', flex: 1 },
            { header: 'State', dataIndex: 'state', flex: 1 }
        ];

		this.callParent(arguments);
	}
});