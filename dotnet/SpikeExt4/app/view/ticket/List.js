Ext.define('Spike.view.ticket.List', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ticketlist',

	initComponent: function () {
		this.store = Ext.create('Spike.store.Tickets');
		
		this.columns = [
            { header: 'Title', dataIndex: 'title', flex: 1 },
            { header: 'Description', dataIndex: 'description', flex: 1 },
            { header: 'State', dataIndex: 'state', flex: 1 }
        ];

		this.callParent(arguments);
	}
});