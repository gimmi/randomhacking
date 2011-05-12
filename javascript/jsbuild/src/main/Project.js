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
		return this._tasks[name];
	},
	getTasks: function (name) {
		var tasks = [];
		this.getTask(name).visit(tasks, new Make.RecursionChecker('Task recursion found'));
		return _(tasks).unique();
	},
	run: function (name) {
		var tasks = this.getTasks(name);
		this._tasks[name].run();
	}
};
