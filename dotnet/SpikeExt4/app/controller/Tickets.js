Ext.define('Spike.controller.Tickets', {
	extend: 'Ext.app.Controller',

	models: ['Ticket', 'FilterClause', 'TaskInfo' ],

	views: [
		'ticket.List',
		'ticket.Filter',
		'ticket.ListFilter',
		'taskinfo.List'
	],

	init: function () {
		this.control({
			'ticket.List': {
				itemdblclick: this.editTicket
			}
		});
	},

	editTicket: function (grid, record) {
		alert('TODO');
	}
});