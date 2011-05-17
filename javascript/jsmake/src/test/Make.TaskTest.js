describe("Make.Task", function() {
	var target, body;

	beforeEach(function() {
		body = jasmine.createSpy();
		target = new Make.Task('a', [ 'b', 'c' ], body);
	});

	it("should return properties", function() {
		expect(target.getName()).toEqual('a');
		expect(target.getTaskNames()).toEqual([ 'b', 'c' ]);
	});

	it("when ran, should return body return value or 0", function() {
		body.andReturn(1);
		expect(target.run()).toEqual(1);

		body.andReturn(undefined);
		expect(target.run()).toEqual(0);
	});
});