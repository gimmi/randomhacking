/*global Make */

Make.Main = function () {
	this._definedProject = null;
	this._currentProject = null;
	this._currentTask = null;
};
Make.Main.prototype = {
	run: function (args) {
		if (!this._definedProject) {
			throw 'No project defined';
		}
		this._definedProject.run(args.shift(), args);
	},
	project: function (name, defaultTaskName, body) {
		if (this._definedProject) {
			throw 'project already defined';
		}
		this._definedProject = this._currentProject = new Make.Project(name, defaultTaskName, Make.Sys);
		body.apply({}, []);
		this._currentProject = null;
	},
	task: function (name, tasks, body) {
		if (!this._currentProject) {
			throw 'Tasks must be defined only into projects';
		}
		this._currentProject.addTask(new Make.Task(name, tasks, body, Make.Sys));
	}
};