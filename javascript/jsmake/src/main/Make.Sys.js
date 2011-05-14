Make.Sys = {
	getCanonicalPath: function (path) {
		return new java.io.File(basePath).getCanonicalPath();
	},
	combinePath: function (path1, path2) {
		return new java.io.File(path1, path2).getPath();
	},
	getFiles: function (basePath) {
		var fileNames = [];
		this._visitPath(basePath, '.', fileNames);
		return fileNames;
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
			this.log(line);
		}
		reader.close();
	},
	log: function (msg) {
		print(msg);
	},
	_visitPath: function (basePath, path, fileNames) {
		var file = new java.io.File(basePath, path);
		if (file.isFile()) {
			fileNames.push(path);
		} else if(file.isDirectory()) {
			Make.each(this._translateJavaArray(new java.io.File(basePath, path).listFiles()), function (file) {
				this._visitPath(basePath, this.combinePath(path, file.getName()), fileNames);
			}, this);
		}
	},
	_translateJavaArray: function (javaArray) {
		var ary = [];
		for (var i = 0; i < javaArray.length; i += 1) {
			ary.push(javaArray[i]);
		}
		return ary;
	}
};