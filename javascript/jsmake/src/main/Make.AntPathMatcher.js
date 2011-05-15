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
		while((pathToken = pathTokens.shift()) !== undefined) {
			if((patternToken = patternTokens.shift()) === undefined) {
				return false;
			} else if (patternToken === '**') {
				if((patternToken = patternTokens.shift()) === undefined) {
					return true;
				}
				while(!this._matchToken(patternToken, pathToken)) {
					if((pathToken = pathTokens.shift()) === undefined) {
						return false;
					}
				}
			} else {
				if (!this._matchToken(patternToken, pathToken)) {
					return false;
				}
			}
		}
		return true;
/*
		while((patternToken = patternTokens.shift()) !== undefined) {
			if (patternToken === '**') {
				if((patternToken = patternTokens.shift()) === undefined) {
					return true;
				}
				do {
					if((pathToken = pathTokens.shift()) === undefined) {
						return false;
					}
				} while(this._matchToken(patternToken, pathToken))
			} else {
				pathToken = pathTokens.shift();
				if (!this._matchToken(patternToken, pathToken)) {
					return false;
				}
			}
		}
		return true;
*/
	},
	_matchToken: function (patternToken, pathToken) {
		return patternToken === pathToken;
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