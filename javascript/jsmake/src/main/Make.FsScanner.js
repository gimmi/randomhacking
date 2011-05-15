Make.FsScanner = function (basePath) {
	this._basePath = basePath;
};
Make.FsScanner.prototype = {
	scan: function () {
		var fileNames = [];
		this._scan('.', fileNames);
		return fileNames;
	},
	_scan: function (relativePath, fileNames) {
		var fullPath = Make.Sys.combinePath(this._basePath, relativePath);
		Make.each(Make.Sys.getFiles(fullPath), function (fileName) {
			fileNames.push(Make.Sys.combinePath(relativePath, fileName));
		}, this);
		Make.each(Make.Sys.getDirectories(fullPath), function (path) {
			this._scan(Make.Sys.combinePath(relativePath, path), fileNames);
		}, this);
	}
};