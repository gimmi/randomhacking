/*global Make, jasmine, describe, beforeEach, expect, it */

describe("Make.Main", function () {
	var target;

	beforeEach(function () {
		target = new Make.Main();
	});

	it("should define project", function () {
		target.project('name', 'defaultTaskName', function () {
			expect(target._currentProject.getName()).toEqual('name');
		});

		expect(target._definedProject.getName()).toEqual('name');
		expect(target._currentProject).toBeNull();
	});

	it("should allow only one project", function () {
		target.project('prj1', 'defaultTaskName', jasmine.createSpy());
		expect(function () {
			target.project('prj2', 'defaultTaskName', jasmine.createSpy());
		}).toThrow('project already defined');
	});

	it("should allow task to be defined only inside project", function () {
		expect(function () {
			target.task('task', [], jasmine.createSpy());
		}).toThrow('Tasks must be defined only into projects');
		target.project('prj1', 'defaultTaskName', function () {
			target.task('task', [], jasmine.createSpy());
		});
		expect(target._definedProject.getTask('task')).not.toBeNull();
	});

	it('should throw error when nesting task inside task', function () {
		target.project('prj1', 'task', function () {
			target.task('task', [], function () {
				target.task('nested task', [], jasmine.createSpy());
			});
		});

		expect(function () {
			target.run([]);
		}).toThrow('Tasks must be defined only into projects');
	});
});