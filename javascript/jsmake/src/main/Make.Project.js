/*global Make */

Make.Project = function (name, logger) {
	this._name = name;
	this._tasks = {};
	this._logger = logger;
};
Make.Project.prototype = {
	addTask: function (task) {
		this._tasks[task.getName()] = task;
	},
	getTask: function (name) {
		var task = this._tasks[name];
		if (!task) {
			throw "Task '" + name + "' not defined";
		}
		return task;
	},
	getTasks: function (name) {
		var tasks = [];
		this._fillDependencies(this.getTask(name), tasks, new Make.RecursionChecker('Task recursion found'));
		return Make.distinct(tasks);
	},
	run: function (name) {
		var tasks, taskNames;
		tasks = this.getTasks(name);
		taskNames = Make.map(tasks, function (task) {
			return task.getName();
		}, this);
		this._logger.log('Runnung tasks ' + taskNames.join(', '));
		Make.each(tasks, function (task) {
			task.run();
		}, this);
	},
	_fillDependencies: function (task, tasks, recursionChecker) {
		recursionChecker.wrap(task.getName(), function () {
			Make.each(task.getTaskNames(), function (taskName) {
				var task = this.getTask(taskName);
				this._fillDependencies(task, tasks, recursionChecker);
			}, this);
			tasks.push(task);
		}, this);
	}
};
