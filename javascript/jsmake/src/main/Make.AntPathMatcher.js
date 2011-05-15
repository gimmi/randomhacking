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
		while(true) {
			pathToken = pathTokens.shift();
			patternToken = patternTokens.shift();
			if (patternToken && pathToken) {
				if (patternToken === '**') {
					patternToken = patternTokens.shift();
					while (!this._matchToken(patternToken, pathToken)) {
						pathToken = pathTokens.shift();
						if (!pathToken) {
							return false;
						}
					}
				} else if (!this._matchToken(patternToken, pathToken)) {
					return false;
				}
			} else if (patternToken && !pathToken) {
				return false;
			} else if (!patternToken && pathToken) {
				return false;
			} else {
				return true;
			}
		}
/*
			if(patternToken === undefined) {
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
*/
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
		var regex = '';
		for (var i = 0; i < patternToken.length; i += 1) {
			var ch = patternToken.charAt(i);
			if (ch === '*') {
				regex += '.*';
			} else if (ch === '?') {
				regex += '.{1}';
			} else {
				regex += Make.escapeForRegex(ch);
			}
		}
		return new RegExp(regex).test(pathToken);
	},
	_tokenize: function (pattern) {
		var tokens = pattern.split(/\\+|\/+/);
		tokens = Make.map(tokens, function (token) {
			return Make.trim(token);
		}, this);
		tokens = Make.filter(tokens, function (token) {
			return !/^[\s\.]*$/.test(token);
		}, this);
		if (tokens[tokens.length - 1] === '**') {
			throw 'Invalid ** wildcard at end pattern';
		}
		// TODO invalid more then one **
		return tokens;
	}
};