Make.AntPathMatcher = function () {
};
Make.AntPathMatcher.prototype = {
	CASE_SENSITIVE: false,
	match: function (pattern, path) {
		var patternTokens = this._tokenize(pattern);
		var pathTokens = this._tokenize(path);
		return this._matchTokens(patternTokens, pathTokens);
	},
	_matchTokens: function (patternTokens, pathTokens) {
		var patternToken;
		var pathToken;
		while((patternToken = patternTokens.shift()) !== undefined) {
			if(patternToken === '**') {
				return this._matchTokens(patternTokens.reverse(), pathTokens.reverse());
			}
			pathToken = pathTokens.shift()
			if(patternToken !== pathToken) {
				return false;
			}
		}
		return true;
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