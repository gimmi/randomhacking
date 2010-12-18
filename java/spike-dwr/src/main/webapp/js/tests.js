/*global describe, beforeEach, it, expect, waitsFor, runs */

"use strict";

describe('RemoteMethods', function () {
	it('Should call rpc methods', function () {
		var callCompleted = false;
		var retVal;
		RemoteMethods.simpleParametersEcho(123, 'str', true, new Date('2010-12-25'), 3.14, function (ret) {
			retVal = ret;
			callCompleted = true;
		});
		
		waitsFor(function () {
			return callCompleted;
		}, 'Server call', 1000);
		
		runs(function () {
			expect(retVal).toEqual('123, str, true, Sat Dec 25 00:00:00 CET 2010, 3.14');
		});

	});
});