Ext.namespace('spikeExt');

spikeExt.Window = function(modal) {
	this.window = new Ext.Window( {
		title : 'Window',
		width : 500,
		height : 300,
		minWidth : 300,
		minHeight : 200,
		layout : 'fit',
		plain : true,
		modal : modal,
//		items : form,
		html : '<p>A Window..</p>',
		buttons : [ {
			text : 'Send'
		}, {
			text : 'Cancel'
		} ]
	});
};

spikeExt.Window.prototype.show = function() {
	this.window.show();
};