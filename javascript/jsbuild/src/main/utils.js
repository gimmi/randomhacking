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

Make = {};

function setupGlobalMethods (global) {
	var project = null;
	
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
};

setupGlobalMethods(this);