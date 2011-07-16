Ext.define('Spike.model.Comment', {
	extend: 'Ext.data.Model',
	fields: ['user', 'text'],
	belongsTo: 'Ticket'
});