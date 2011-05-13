(function (global) {
	var project = null;
	
	global.Make = {};
	
	global.project = function (name, defaultTaskName, body) {
		project = new Make.Project(name);
		try {
			body();
		} finally {
			project = null;
		}
		project.run(defaultTaskName);
	};

	global.task = function (name, tasks, body) {
		project.addTask(new Make.Task(project, name, tasks, body));
	};
}(this));