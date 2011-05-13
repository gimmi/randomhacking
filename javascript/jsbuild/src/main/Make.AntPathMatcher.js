Make.AntPathMatcher = function () {
};
Make.AntPathMatcher.prototype = {
	PATH_SEPARATOR: '/',
	isPattern: function (path) {
		return (/\*|\?/).test(path);
	}
};