/*global Make */

(function (global, args) {
	var currentProject = null;

	global.project = function (name, defaultTaskName, body) {
		var project = currentProject = new Make.Project(name, Make.Sys);
		body.apply({}, []);
		currentProject = null;
		project.run(args.shift() || defaultTaskName, args);
	};

	global.task = function (name, tasks, body) {
		currentProject.addTask(new Make.Task(name, tasks, body, Make.Sys));
	};
}(this, arguments));