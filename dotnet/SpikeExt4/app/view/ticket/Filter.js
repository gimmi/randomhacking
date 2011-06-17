Ext.define('Spike.view.ticket.Filter', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ticket.Filter',

	title: 'Ticket Filters',

	initComponent: function () {
		this.store = Ext.create('Spike.store.FilterClauses');
		
		this.columns = [
            { header: 'Field', dataIndex: 'field' },
            { header: 'Operator', dataIndex: 'operator' },
            { header: 'Value', dataIndex: 'value' }
        ];

		this.callParent(arguments);
	}
});