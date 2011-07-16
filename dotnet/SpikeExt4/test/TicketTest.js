describe("Tests", function () {
	var target;

	beforeEach(function () {
		target = {};
	});

	it("should be able to play a Song", function () {
		//var ticket = Ext.create('Spike.model.Ticket');
		var ticketStore = Ext.create('Ext.data.Store', {
			model: 'Spike.model.Ticket',
			data: [{
				id: 1,
				title: 'Title 1',
				description: 'Descr 1',
				state: 'Open',
				comments: [
					{ id: 1, user: 'Gimmi', text: 'a text' }
				]
			}]
		});
		expect(ticketStore.getCount()).toEqual(1);
		var ticket = ticketStore.getAt(0);
		expect(ticket.get('title')).toEqual('Title 1');
		var commentStore = ticket.comments();
		commentStore.load();
		expect(commentStore.getCount()).toEqual(1);
		var comment = commentStore.getAt(0);
		//expect(comment).toEqual(1);
		// var ticket = Ext.create('Spike.model.Ticket');
	});
});