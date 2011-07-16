Ext.define('Spike.controller.Tickets', {
	extend: 'Ext.app.Controller',

	models: ['Ticket', 'Comment', 'FilterClause', 'TaskInfo'],

	views: [
		'ticket.List',
		'ticket.Filter',
		'ticket.ListFilter',
		'taskinfo.List'
	],

	init: function () {
		this.control({
			'ticketlist': {
				render: this.onTicketListRender,
				itemdblclick: this.editTicket
			}
		});
	},

	onTicketListRender: function (sender) {
		sender.getStore().load();
	},

	editTicket: function (grid, record) {
		alert('TODO');
	}
});