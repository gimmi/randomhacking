/*global Make, jasmine, describe, beforeEach, expect, it */

describe("Make.Project", function () {
	var target, logger;

	beforeEach(function () {
		logger = jasmine.createSpyObj('logger', [ 'log' ]);
		target = new Make.Project('test project', logger);
	});

	function createTask(name, tasks, fn) {
		return target.addTask(new Make.Task(name, tasks, fn));
	}

	function getTaskNames(tasks) {
		return Make.map(target.getTasks('t1'), function (task) {
			return task.getName();
		});
	}

	it("Should find dependent tasks with correct run order", function () {
		createTask('t1', ['t1.1', 't1.2']);
		createTask('t1.1', ['t1.1.1', 't1.1.2']);
		createTask('t1.1.1', []);
		createTask('t1.1.2', []);
		createTask('t1.2', ['t1.2.1', 't1.2.2']);
		createTask('t1.2.1', []);
		createTask('t1.2.2', []);

		var actual = getTaskNames(target.getTasks('t1'));

		expect(actual).toEqual([ 't1.1.1', 't1.1.2', 't1.1', 't1.2.1', 't1.2.2', 't1.2', 't1' ]);
	});

	it("Should remove duplicate tasks", function () {
		createTask('t1', ['t1.1', 't1.2']);
		createTask('t1.1', ['t dupl']);
		createTask('t1.2', ['t dupl']);
		createTask('t dupl', []);

		var actual = getTaskNames(target.getTasks('t1'));

		expect(actual).toEqual([ 't dupl', 't1.1', 't1.2', 't1' ]);
	});

	it('should detect recursive task reference', function () {
		createTask('t1', ['t2']);
		createTask('t2', ['t3']);
		createTask('t3', ['t1']);

		expect(function () {
			target.getTasks('t1');
		}).toThrow('Task recursion found: t1 => t2 => t3 => t1');
	});

	it('should run tasks in dependency order', function () {
		var execution = [];
		createTask('t1', [ 't2', 't3' ], function () {
			execution.push(1);
		});
		createTask('t2', [], function () {
			execution.push(2);
		});
		createTask('t3', [], function () {
			execution.push(3);
		});

		target.run('t1');

		expect(execution).toEqual([ 2, 3, 1 ]);
		expect(logger.log).toHaveBeenCalledWith('Runnung tasks t2, t3, t1');
	});

	it('should throw exception when trying to get task that does not exists', function () {
		expect(function () {
			target.getTask('a task');
		}).toThrow("Task 'a task' not defined");
	});
});