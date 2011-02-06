"use strict";

BooleanConverter = function () {
};

BooleanConverter.prototype.toBoolean = function (value) {
	return !!value;
};