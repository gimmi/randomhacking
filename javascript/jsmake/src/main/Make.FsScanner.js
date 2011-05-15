Make.FsScanner = function (basePath) {
	this._basePath = basePath;
	this._includeMatchers = [];
	this._excludeMatchers = [];
};
Make.FsScanner.prototype = {
	include: function (pattern) {
		this._includeMatchers.push(new Make.AntPathMatcher(pattern));
		return this;
	},
	exclude: function (pattern) {
		this._excludeMatchers.push(new Make.AntPathMatcher(pattern));
		return this;
	},
	scan: function () {
		var fileNames = [];
		this._scan('.', fileNames);
		return fileNames;
	},
	_scan: function (relativePath, fileNames) {
		var fullPath = Make.Sys.combinePath(this._basePath, relativePath);
		Make.each(Make.Sys.getFiles(fullPath), function (fileName) {
			fileName = Make.Sys.combinePath(relativePath, fileName);
			if (this._evaluateInclusion(fileName)) {
				fileNames.push(fileName);
			}
		}, this);
		Make.each(Make.Sys.getDirectories(fullPath), function (path) {
			path = Make.Sys.combinePath(relativePath, path);
			if (this._evaluateInclusion(path)) {
				this._scan(path, fileNames);
			}
		}, this);
	},
	_evaluateInclusion: function (path) {
		var exclude = false;
		Make.each(this._excludeMatchers, function (matcher) {
			if(matcher.match(path)) {
				exclude = true;
				return true;
			}
		}, this);
		var include = false;
		Make.each(this._includeMatchers, function (matcher) {
			if(matcher.match(path)) {
				include = true;
				return true;
			}
		}, this);
		return (include && !exclude);
	}
};