"use strict";

com = {
	github: {
		gimmi: {
			spikemavenjs: {}
		}
	}
};

com.github.gimmi.spikemavenjs.ClassUnderTest = function () {
};

com.github.gimmi.spikemavenjs.ClassUnderTest.prototype.toBoolean = function (value) {
	return !!value;
};