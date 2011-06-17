Ext.require('Ext.direct.Manager');

Ext.onReady(function () {
	Ext.direct.Manager.addProvider(Spike.server.REMOTING_API);

	Ext.create('Ext.app.Application', {
		name: 'Spike',
		autoCreateViewport: true,
		controllers: ['Users'],
		launch: function () {
			console.log('app launched');
		}
	});
});
