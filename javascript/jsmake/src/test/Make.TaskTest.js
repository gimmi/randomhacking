/*global Make, jasmine, describe, beforeEach, expect, it */

describe("Make.Task", function () {
	var target, body, logger;

	beforeEach(function () {
		logger = jasmine.createSpyObj('logger', [ 'log' ]);
		body = jasmine.createSpy();
		target = new Make.Task('a', [ 'b', 'c' ], body, logger);
	});

	it("should return properties", function () {
		expect(target.getName()).toEqual('a');
		expect(target.getTaskNames()).toEqual([ 'b', 'c' ]);
	});

	it("should log and execute body when run", function () {
		target.run();

		expect(body).toHaveBeenCalled();
		expect(logger.log).toHaveBeenCalledWith('Executing task a');
	});
});