Make.AntPathMatcher = function () {
};
Make.AntPathMatcher.prototype = {
	CASE_SENSITIVE: false,
	match: function (pattern, path) {
		var patternParts = this._normalize(pattern);
		var pathParts = this._normalize(path);
		// Make.each(patternParts, 
	},
	_normalize: function (pattern) {
		var parts = pattern.split(/\\+|//+/);
		parts = Make.map(parts, function (part) {
			return Make.trim(part);
		}, this);
		parts = Make.filter(parts, function (part) {
			return (part.length > 0) && (part !== '.') && (part !== '..');
		}, this);
		return parts;
	},
	_split: function (parts) {
		var head = current = [], tail = [];
		Make.each(parts, function (part) {
			if (part === '**') {
				current = tail;
			} else {
				current.push(part);
			}
		}, this);
		return { head: head, tail: tail };
	}
};