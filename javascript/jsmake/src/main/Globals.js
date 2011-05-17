/*global Make */

(function (global) {
	var currentProject = null;

	global.project = function (name, defaultTaskName, body) {
		var project = new Make.Project(name);
		currentProject = project;
		try {
			body();
		} finally {
			currentProject = null;
		}
		project.run(defaultTaskName);
	};

	global.task = function (name, tasks, body) {
		currentProject.addTask(new Make.Task(name, tasks, body));
	};
}(this));