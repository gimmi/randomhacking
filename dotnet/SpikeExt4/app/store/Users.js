Ext.define('Spike.store.Users', {
	extend: 'Ext.data.Store',
	model: 'Spike.model.User',
	data: [
        { name: 'Ed', email: 'ed@sencha.com' },
        { name: 'Tommy', email: 'tommy@sencha.com' }
    ]
});