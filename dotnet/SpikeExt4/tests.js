Ext.Loader.setPath('Spike', 'app');

Ext.require('Ext.direct.Manager');

Ext.require('Spike.controller.Tickets');

Ext.onReady(function() {
	Ext.direct.Manager.addProvider(Spike.server.REMOTING_API);

	jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
	jasmine.getEnv().execute();
});
