/*global Make */

Make.Task = function (name, taskNames, body) {
	this._name = name;
	this._taskNames = taskNames;
	this._body = body;
};
Make.Task.prototype = {
	getName: function () {
		return this._name;
	},
	getTaskNames: function () {
		return this._taskNames;
	},
	run: function () {
		return this._body() || 0;
	}
};