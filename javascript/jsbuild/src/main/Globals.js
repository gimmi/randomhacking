(function (global) {
	var project = null;
	
	global.Make = {};
	
	global.project = function (name, defaultTaskName, body) {
		project = new Make.Project(name);
		body();
		project = null;
		project.run(defaultTaskName);
	}

	global.task = function (name, tasks, body) {
		var task = new Make.Task(project, name, tasks, body);
		project.addTask(task);
	}
}(this));