describe("Make.Project", function() {
	var target;
	beforeEach(function() {
		target = new Make.Project('test project');
	});
	
	function createTask (name, tasks) {
		return target.addTask(new Make.Task(target, name, tasks, jasmine.createSpy()));
	}

	it("Should find dependent tasks with correct run order", function() {
		createTask('t1', ['t1.1', 't1.2']);
		createTask('t1.1', ['t1.1.1', 't1.1.2']);
		createTask('t1.1.1', []);
		createTask('t1.1.2', []);
		createTask('t1.2', ['t1.2.1', 't1.2.2']);
		createTask('t1.2.1', []);
		createTask('t1.2.2', []);
		
		var actual = _(target.getTasks('t1')).map(function (task) {
			return task.getName();
		});
		
		expect(actual).toEqual([ 't1.1.1', 't1.1.2', 't1.1', 't1.2.1', 't1.2.2', 't1.2', 't1' ]);
	});
	
	it('should detect recursive task reference', function () {
		createTask('t1', ['t2']);
		createTask('t2', ['t3']);
		createTask('t3', ['t1']);
		
		expect(function () {
			target.getTasks('t1');
		}).toThrow('Task recursion found. t1 => t2 => t3 => t1');
	});
});