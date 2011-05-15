Make.AntPathMatcher = function () {
};
Make.AntPathMatcher.prototype = {
	CASE_SENSITIVE: false,
	match: function (pattern, path) {
		pattern = this._split(this._normalize(pattern));
		path = this._normalize(path);
		for (var i = 0, match = true; i < path.length && match === true; i += 1) {
			path[i] === pattern[i]
		}
		Make.each(path, function (part) {
			
		}, this);
		var ret = true;
		Make.each(path.headParts, function (part, index) {
			return !(ret = ret && part === path[index]);
		}, this);
		Make.each(path.tailParts, function (part, index) {
			return !(ret = ret && part === path[index]);
		}, this);
		// Make.each(patternParts, 
	},
	_toRegexes: function (tokens) {
		var regexes = [];
		Make.each(tokens, function(token, index, tokens) {
			regexes.push(this._toRegex(tokens.slice(0, index + 1)));
		}, this);
		return regexes;
	},
	_toRegex: function (tokens) {
		'^' + Make.join(tokens, '\/')
	},
	_tokenize: function (pattern) {
		var tokens = pattern.split(/\\+|\/+/);
		tokens = Make.map(tokens, function (token) {
			return Make.trim(token);
		}, this);
		tokens = Make.filter(tokens, function (token) {
			return !/^[\s\.]*$/.test(token);
		}, this);
		return tokens;
	},
	_normalize: function (pattern) {
		return Make.join(this._tokenize(pattern), '/');
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
		return { headParts: head, tailParts: tail };
	}
};