Ext.define('Spike.view.ticket.List', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ticketlist',
		
	store: 'Tickets',

	title: 'All Tickets',

	initComponent: function () {
		this.columns = [
            { header: 'Name', dataIndex: 'name', flex: 1 },
            { header: 'Email', dataIndex: 'email', flex: 1 }
        ];

		this.callParent(arguments);
	}
});