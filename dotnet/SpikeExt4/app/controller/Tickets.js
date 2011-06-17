Ext.define('Spike.controller.Tickets', {
	extend: 'Ext.app.Controller',

	models: ['Ticket', 'FilterClause'],

	stores: ['Tickets', 'FilterClauses'],

	views: [
		'ticket.List',
		'ticket.Filter',
		'ticket.ListFilter'
	],

	init: function () {
		this.control({
			'ticketlist': {
				itemdblclick: this.editTicket
			}
		});
	},

	editTicket: function (grid, record) {
		alert('TODO');
	}
});