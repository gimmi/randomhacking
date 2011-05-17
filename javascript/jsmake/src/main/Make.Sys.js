/*jslint rhino: true */
/*global Make, java */

Make.Sys = {
	readFile: function (path) {
		return readFile(path);
	},
	createDirectories: function (path) {
		if (!new java.io.File(path).mkdirs()) {
			throw "Failed to create directories for path '" + path + "'";
		}
	},
	getCanonicalPath: function (path) {
		return this._translateJavaString(new java.io.File(path).getCanonicalPath());
	},
	combinePath: function (path1, path2) {
		return this._translateJavaString(new java.io.File(path1, path2).getPath());
	},
	getFiles: function (basePath) {
		return this._getFiles(basePath, function (fileName) {
			return new java.io.File(fileName).isFile();
		});
	},
	getDirectories: function (basePath) {
		return this._getFiles(basePath, function (fileName) {
			return new java.io.File(fileName).isDirectory();
		});
	},
	runCmd: function (cmd) {
		var process = java.lang.Runtime.getRuntime().exec(cmd);

		this.printInputStream(process.getInputStream());
		this.printInputStream(process.getErrorStream());

		return process.exitValue();
	},
	printInputStream: function (inputStream) {
		var reader, line;
		reader = new java.io.BufferedReader(new java.io.InputStreamReader(inputStream));
		while ((line = reader.readLine()) !== null) {
			this.log(line);
		}
		reader.close();
	},
	log: function (msg) {
		print(msg);
	},
	_getFiles: function (basePath, filter) {
		var fileFilter, files;
		fileFilter = new java.io.FileFilter({ accept: filter });
		files = this._translateJavaArray(new java.io.File(basePath).listFiles(fileFilter));
		return Make.map(files, function (file) {
			return this._translateJavaString(file.getName());
		}, this);
	},
	_translateJavaArray: function (javaArray) {
		var ary = [], i;
		for (i = 0; i < javaArray.length; i += 1) {
			ary.push(javaArray[i]);
		}
		return ary;
	},
	_translateJavaString: function (javaString) {
		return String(javaString);
	}
};