Make.AntPathMatcher = function () {
};
Make.AntPathMatcher.prototype = {
	CASE_SENSITIVE: false,
	match: function (pattern, basePath) {
		var parts = this._parsePattern(pattern);
		var file = new java.io.File(basePath);
	},
	_parsePattern: function (pattern) {
		pattern = pattern.replace(/(?:\s*\\+\s*)|(?:\s*\/+\s*)/g, '/');
		if(pattern.indexOf('/') === 0) {
			throw 'Absolute path patterns not supported';
		}
		return Make.map(pattern.split('/'), function (part) {
			return Make.trim(part);
		}, this);
	},
	_makeRelative: function (basePath, path) {
		basePath = Make.Sys.getCanonicalPath(basePath) + '/';
		path = Make.Sys.getCanonicalPath(path);
		if (path.indexOf(basePath) !== 0) {
			throw "'" + path + "' is not a subpath of '" + basePath + "'";
		}
		return path.slice(basePath.length - 1);
	}
};