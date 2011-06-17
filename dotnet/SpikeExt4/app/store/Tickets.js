Ext.define('Spike.store.Tickets', {
	extend: 'Ext.data.Store',
	model: 'Spike.model.Ticket',
	autoLoad: true,
	proxy: {
		type: 'direct',
		api: {
			read: Spike.server.TicketRepository.getAll
		}
	}
});