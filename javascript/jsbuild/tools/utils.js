Helpers = {
	iterateArray: function (items, fn, scope) {
		var i;
		for (i = 0; i < items.length; i += 1) {
			fn.call(scope, items[i]);
		}
	},
	forEachFile: function (path, regex, fn, scope) {
		this.iterateArray(new java.io.File(path).listFiles(), function (file) {
			var filePath = file.getPath();
			if (file.isDirectory()) {
				this.forEachFile(filePath, regex, fn, scope);
			} else if (file.isFile() && regex.test(filePath)) {
				fn.call(scope, filePath);
			}
		}, this);
	},
	runCmd: function (cmd) {
		var process = java.lang.Runtime.getRuntime().exec(cmd);

		this.printInputStream(process.getInputStream());
		this.printInputStream(process.getErrorStream());

		return process.exitValue();
	},
	printInputStream: function (inputStream) {
		var reader = new java.io.BufferedReader(new java.io.InputStreamReader(inputStream));
		var line;
		while (line = reader.readLine()) {
			print(line);
		}
		reader.close();
	}
};

Make = {
	project: null
};
Make.Project = function (name) {
	this._name = name;
	this._tasks = {};
};
Make.Project.prototype.addTask = function (task) {
	this._tasks[task.getName()] = task;
};
Make.Project.prototype.run = function (taskName) {
	this._tasks[taskName].run();
};

Make.Task = function (name, taskNames, body) {
	this._name = name;
	this._taskNames = taskNames;
	this._body = body;
};
Make.Task.prototype.getName = function () {
	return this._name;
};
Make.Task.prototype.run = function () {
	this._body();
};

function project(name, defaultTaskName, body) {
	var project = new Make.Project(name);
	Make.project = project;
	body();
	Make.project = null;
	project.run(defaultTaskName);
}

function task(name, tasks, body) {
	var task = new Make.Task(name, tasks, body);
	Make.project.addTask(task);
}

