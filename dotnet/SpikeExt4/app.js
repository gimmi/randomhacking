Ext.application({
	name: 'Spike',
	autoCreateViewport: true,
	controllers: [ 'Users' ],
	launch: function () {
		console.log('app launched');
	}
});
