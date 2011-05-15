Make.AntPathMatcher = function () {
};
Make.AntPathMatcher.prototype = {
	CASE_SENSITIVE: false,
	fileMatch: function (pattern, path) {
		return this.match(pattern, path, false);
	},
	directoryMatch: function (pattern, path) {
		return this.match(pattern, path, true);
	},
	match: function (pattern, path, partialMatch) {
		var patternTokens = this._tokenize(pattern);
		var pathTokens = this._tokenize(path);
		return this._matchTokens(patternTokens, pathTokens, partialMatch);
	},
	_matchTokens: function (patternTokens, pathTokens, partialMatch) {
		var patternToken;
		var pathToken;
		while(true) {
			patternToken = patternTokens.shift();
			if (patternToken === '**') {
				pathTokens = pathTokens.slice(-patternTokens.length).reverse();
				patternTokens = patternTokens.reverse();
				return this._matchTokens(patternTokens, pathTokens, partialMatch);
			}
			pathToken = pathTokens.shift();
			if (patternToken && pathToken) {
				if (!this._matchToken(patternToken, pathToken)) {
					return false;
				}
			} else if (patternToken && !pathToken) {
				return partialMatch;
			} else if (!patternToken && pathToken) {
				return false;
			} else {
				return true;
			}
		}
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
			throw 'Invalid ** wildcard at end pattern, use **/* instead';
		}
		// TODO invalid more then one **
		return tokens;
	}
};