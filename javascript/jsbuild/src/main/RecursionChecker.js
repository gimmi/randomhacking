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
		if(_(this._stack).contains(id)) {
			throw _(this._stack).reduce(function(message, id) {
				return message + id + ' => ';
			}, this._message + '. ') + id;
		}
	}
};