/*global describe, beforeEach, it, expect, waitsFor, runs */

"use strict";

describe('Spike spec', function () {
	var target;
	
	beforeEach(function () {
		target = new com.github.gimmi.spikemavenjs.ClassUnderTest();
	});
	
	it('Should fail', function () {
		expect(target.toBoolean(false)).toBeTruthy();
	});

	it('Should succeed', function () {
		expect(target.toBoolean(true)).toBeTruthy();
	});
});