Ext.define('Spike.store.Users', {
	extend: 'Ext.data.Store',
	model: 'Spike.model.User',
	autoLoad: true,
	proxy: {
		type: 'direct',
		api: {
			read: Spike.server.TicketRepository.getAll
		}
	}
});