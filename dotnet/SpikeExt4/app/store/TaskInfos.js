Ext.define('Spike.store.TaskInfos', {
	extend: 'Ext.data.Store',
	model: 'Spike.model.TaskInfo',
	autoLoad: true,
	proxy: {
		type: 'direct',
		api: {
			read: Spike.server.TicketRepository.getAllInfo
		}
	}
});