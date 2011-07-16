describe("Tests", function () {
	var target;

	beforeEach(function () {
		target = {};
	});

	it("should be able to play a Song", function () {
		//var ticket = Ext.create('Spike.model.Ticket');
		var store = Ext.create('Ext.data.Store', {
			model: 'Spike.model.Ticket',
			data: [
				{ title: 'Title 1', description: 'Descr 1', state: 'Open' },
				{ title: 'Title 2', description: 'Descr 2', state: 'Open' },
				{ title: 'Title 3', description: 'Descr 3', state: 'Open' }
			]
		});
		expect(store.getCount()).toEqual(3);
		// var ticket = Ext.create('Spike.model.Ticket');
	});
});