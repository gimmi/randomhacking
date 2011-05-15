Make.RecursionChecker = function (message) {
	this._message = message;
	this._stack = [];
};
Make.RecursionChecker.prototype = {
	enter: function (id) {
		this._check(id);
		this._stack.push(id);
	},
	exit: function () {
		this._stack.pop();
	},
	wrap: function (id, fn, scope) {
		this.enter(id);
		try {
			fn.call(scope);
		} finally {
			this.exit();
		}
	},
	_check: function (id) {
		if(Make.contains(this._stack, id)) {
			throw this._message + ': ' + Make.join(this._stack, ' => ') + ' => ' + id;
		}
	}
};