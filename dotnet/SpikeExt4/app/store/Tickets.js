Ext.define('Spike.store.Tickets', {
	extend: 'Ext.data.Store',
	model: 'Spike.model.Ticket',
	// autoLoad: true,
	proxy: {
		type: 'direct',
		batchActions: false,
		api: {
			create: Spike.server.TicketRepository.create,
			read: Spike.server.TicketRepository.read,
			update: Spike.server.TicketRepository.update,
			destroy: Spike.server.TicketRepository.destroy
		}
	}
});