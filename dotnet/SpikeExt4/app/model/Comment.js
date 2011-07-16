Ext.define('Spike.model.Comment', {
	extend: 'Ext.data.Model',
	fields: ['id', 'user', 'text'],
	belongsTo: { model: 'Spike.model.Ticket', name: 'ticket' }
});