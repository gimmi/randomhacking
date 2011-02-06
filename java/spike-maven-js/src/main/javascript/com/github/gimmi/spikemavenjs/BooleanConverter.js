"use strict";

var com = {
	github: {
		gimmi: {
			spikemavenjs: {}
		}
	}
};

com.github.gimmi.spikemavenjs.BooleanConverter = function () {
};

com.github.gimmi.spikemavenjs.BooleanConverter.prototype.toBoolean = function (value) {
	return !!value;
};
