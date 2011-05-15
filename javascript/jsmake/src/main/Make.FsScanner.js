Make.FsScanner = function (basePath) {
	this._basePath = basePath;
	this._includePatterns = [];
	this._excludePatterns = [];
};
Make.FsScanner.prototype = {
	include: function (pattern) {
		this._includePatterns.push(new Make.AntPathMatcher(pattern));
		return this;
	},
	exclude: function (pattern) {
		this._excludePatterns.push(new Make.AntPathMatcher(pattern));
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
			if (this._include(fileName)) {
				fileNames.push(fileName);
			}
		}, this);
		Make.each(Make.Sys.getDirectories(fullPath), function (path) {
			path = Make.Sys.combinePath(relativePath, path);
			if (this._include(path)) {
				this._scan(path, fileNames);
			}
		}, this);
	},
	_include: function (path) {
		var exclude = false;
		Make.each(this._excludePatterns, function (matcher) {
			if(matcher.match(path)) {
				exclude = true;
				return true;
			}
		}, this);
		var include = false;
		Make.each(this._includePatterns, function (matcher) {
			if(matcher.match(path)) {
				include = true;
				return true;
			}
		}, this);
		return (include && !exclude);
	}
};