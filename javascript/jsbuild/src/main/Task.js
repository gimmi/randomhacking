Make.Task = function (project, name, taskNames, body) {
	this._project = project;
	this._name = name;
	this._taskNames = taskNames;
	this._body = body;
};
Make.Task.prototype = {
	getName: function () {
		return this._name;
	},
	visit: function (tasks, recursionChecker) {
		recursionChecker.wrap(this._name, function () {
			_(this._taskNames).each(function (taskName) {
				var task = this._project.getTask(taskName);
				task.visit(tasks, recursionChecker);
			}, this);
			tasks.push(this);
		}, this);
	},
	run: function () {
		this._body();
	}
};