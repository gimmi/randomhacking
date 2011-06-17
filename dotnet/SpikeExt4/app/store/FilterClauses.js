Ext.define('Spike.store.FilterClauses', {
	extend: 'Ext.data.Store',
	model: 'Spike.model.FilterClause',
	autoLoad: true,
	proxy: {
		type: 'direct',
		api: {
			read: Spike.server.FilterClauseRepository.getAll
		}
	}
});