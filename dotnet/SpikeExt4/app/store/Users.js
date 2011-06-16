Ext.define('Spike.store.Users', {
	extend: 'Ext.data.Store',
	model: 'Spike.model.User',
	autoLoad: true,
	proxy: {
		type: 'ajax',
		url: 'users.json',
		reader: {
			type: 'json',
			root: 'users',
			successProperty: 'success'
		}
	}
});