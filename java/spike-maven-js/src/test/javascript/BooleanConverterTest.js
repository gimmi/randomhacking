/*global describe, beforeEach, it, expect, waitsFor, runs */

"use strict";

describe('BooleanConverter', function () {
	var target;
	
	beforeEach(function () {
		target = new BooleanConverter();
	});
	
	it('Should fail', function () {
		expect(target.toBoolean(false)).toBeTruthy();
	});

	it('Should succeed', function () {
		expect(target.toBoolean(true)).toBeTruthy();
	});
});