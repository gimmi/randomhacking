Make = {
	isArray: function(v) {
		return toString.apply(v) === '[object Array]';
	},
	isObject : function(v) {
		return !!v && Object.prototype.toString.call(v) === '[object Object]';
	},
	isEmpty : function(v) {
		return v === null || v === undefined || ((this.isArray(v) && !v.length));
	},
	each: function (items, fn, scope) {
		var key;
		if (items === null || items === undefined) {
			return;
		}
		if (this.isObject(items)) {
			for (key in items) {
				if (items.hasOwnProperty(key)) {
					if (fn.call(scope, items[key], key, items)) {
						return;
					}
				}
			}
		} else if (this.isArray(items)) {
			for(key = 0; key < items.length; key += 1) {
				if (fn.call(scope, items[key], key, items)) {
					return;
				}
			}
		} else {
			fn.call(scope, items, undefined, items);
		}
	},
	select: function (items, fn, scope) {
		var ret = [];
		this.each(items, function (item) {
			if (fn.call(scope, item)) {
				ret.push(item);
			}
		}, this);
		return ret;
	},
	map: function (items, fn, scope) {
		var ret = [];
		this.each(items, function (item, key) {
			ret.push(fn.call(scope, item, key, items));
		}, this);
		return ret;
	},
	join: function (items, separator) {
		var ret = '';
		this.each(items, function (item) {
			ret += (ret === '' ? '' : separator);
			ret += item;
		}, this);
		return ret;
	},
	contains: function (items, item) {
		var ret = false;
		this.each(items, function (it) {
			ret = (it === item);
			return ret;
		}, this);
		return ret;
	},
	distinct: function (items) {
		var ret = [];
		this.each(items, function (item) {
			if (!this.contains(ret, item)) {
				ret.push(item);
			}
		}, this);
		return ret;
	},
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


