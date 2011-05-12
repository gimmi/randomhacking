Make.Project = function (name) {
	this._name = name;
	this._tasks = {};
};
Make.Project.prototype = {
	addTask: function (task) {
		this._tasks[task.getName()] = task;
	},
	getTask: function (name) {
		return this._tasks[name];
	},
	getTasks: function (name) {
		var tasks = [];
		this.getTask(name).visit(tasks, new Make.RecursionChecker('Task recursion found'));
		return tasks;
	},
	run: function (taskName) {
		this._tasks[taskName].run();
	}
};
