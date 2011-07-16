Ext.define('Spike.model.Ticket', {
	extend: 'Ext.data.Model',
	fields: ['id', 'title', 'description', 'state'],
	hasMany: { model: 'Spike.model.Comment', name: 'comments' }
});