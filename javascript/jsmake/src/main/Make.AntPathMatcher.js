Make.AntPathMatcher = function () {
};
Make.AntPathMatcher.prototype = {
	CASE_SENSITIVE: false,
	fileMatch: function (pattern, path) {
		return this._match(pattern, path, true);
	},
	directoryMatch: function (pattern, path) {
		return this._match(pattern, path, false);
	},
	_match: function (pattern, path, exact) {
		var patternTokens = this._tokenize(pattern);
		var pathTokens = this._tokenize(path);
		return this._matchTokens(patternTokens, pathTokens, exact);
	},
	_matchTokens: function (patternTokens, pathTokens, exact) {
		var patternToken;
		var pathToken;
		while((patternToken = patternTokens.shift()) !== undefined && (exact || (pathToken = pathTokens.shift()) !== undefined)) {
			if (exact) {
				pathToken = pathTokens.shift();
			}
			if (patternToken === '**') {
				return this._matchTokens(patternTokens.reverse(), pathTokens.reverse(), exact);
			}
			if (patternToken !== pathToken) {
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
	}
};