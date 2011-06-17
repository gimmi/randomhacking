Ext.define('Spike.view.ticket.Filter', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.ticketfilter',

	store: 'FilterClauses',

	title: 'Ticket Filters',

	initComponent: function () {
		this.columns = [
            { header: 'Field', dataIndex: 'field' },
            { header: 'Operator', dataIndex: 'operator' },
            { header: 'Value', dataIndex: 'value' }
        ];

		this.callParent(arguments);
	}
});