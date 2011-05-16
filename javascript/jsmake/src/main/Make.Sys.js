Make.Sys = {
	readFileToString: function (path) {
		var inputStream = new java.io.FileInputStream(path);
		try {
			var stringWriter = new java.io.StringWriter();
			var inputStreamReader = new java.io.InputStreamReader(inputStream);
			var buffer = java.lang.reflect.Array.newInstance(java.lang.Character.TYPE, 4096);
			var n = 0;
			while (-1 !== (n = inputStreamReader.read(buffer))) {
				stringWriter.write(buffer, 0, n);
			}
			return this._translateJavaString(stringWriter.toString());
		} finally {
			try {
				if (inputStream) {
					inputStream.close();
				}
			} catch (ioe) {
				// ignore
			}
		}
	},
	getCanonicalPath: function (path) {
		return this._translateJavaString(new java.io.File(basePath).getCanonicalPath());
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
	_getFiles: function (basePath, filter) {
		var fileFilter = new java.io.FileFilter({ accept: filter });
		var files = this._translateJavaArray(new java.io.File(basePath).listFiles(fileFilter));
		return Make.map(files, function (file) {
			return this._translateJavaString(file.getName());
		}, this);
	},
	_translateJavaArray: function (javaArray) {
		var ary = [];
		for (var i = 0; i < javaArray.length; i += 1) {
			ary.push(javaArray[i]);
		}
		return ary;
	},
	_translateJavaString: function (javaString) {
		return String(javaString);
	}
};