Ext.define('Spike.view.ticket.List', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ticket.List',

	initComponent: function () {
		this.store = Ext.create('Spike.store.Tickets');
		
		this.columns = [
            { header: 'Title', dataIndex: 'title' },
            { header: 'Description', dataIndex: 'description' },
            { header: 'State', dataIndex: 'state' }
        ];

		this.callParent(arguments);
	}
});