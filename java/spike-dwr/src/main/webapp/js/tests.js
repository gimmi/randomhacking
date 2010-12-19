/*global describe, beforeEach, it, expect, waitsFor, runs */

"use strict";

describe('RemoteMethods', function () {
	it('simpleParametersEcho', function () {
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

	it('dateEcho', function () {
		var callCompleted = false;
		var retVal;
		RemoteMethods.dateEcho(new Date('2010-12-25'), function (ret) {
			retVal = ret;
			callCompleted = true;
		});

		waitsFor(function () {
			return callCompleted;
		}, 'Server call', 1000);

		runs(function () {
			expect(retVal).toEqual(new Date('2010-12-25'));
			expect(retVal instanceof Date).toBeTruthy();
		});

	});

	it('mapWithIntegerKeys', function () {
		var callCompleted = false;
		var retVal;
		RemoteMethods.mapWithIntegerKeys({
			1: 'one',
			2: 'two'
		}, function (ret) {
			retVal = ret;
			callCompleted = true;
		});

		waitsFor(function () {
			return callCompleted;
		}, 'Server call', 1000);

		runs(function () {
			expect(retVal).toEqual({
				1: 'one',
				2: 'two',
				123: 'from java'
			});
		});

	});

	it('stronglyTypedListOfMap', function () {
		var callCompleted = false;
		var retVal;
		RemoteMethods.stronglyTypedListOfMap([ {
			1: new Date('2010-12-01'),
			2: new Date('2010-12-02')
		}, {
			3: new Date('2010-12-03'),
			4: new Date('2010-12-04')
		} ], function (ret) {
			retVal = ret;
			callCompleted = true;
		});

		waitsFor(function () {
			return callCompleted;
		}, 'Server call', 1000);

		runs(function () {
			expect(retVal).toEqual([ {
				1: new Date('2010-12-01'),
				2: new Date('2010-12-02')
			}, {
				3: new Date('2010-12-03'),
				4: new Date('2010-12-04')
			}, {
				123: new Date('2010-12-23')
			} ]);
		});
	});

	it('databaseRows', function () {
		var callCompleted = false;
		var retVal;

		var parameter = [ {
			intValue: 123,
			stringValue: 'aString',
			boolValue: true,
			dateValue: new Date('2010-12-02'),
			doubleValue: 3.14,
			lookupValue: {
				value: 456,
				descr: '456 descr'
			}
		} ];

		RemoteMethods.databaseRows(parameter, function (ret) {
			retVal = ret;
			callCompleted = true;
		});

		waitsFor(function () {
			return callCompleted;
		}, 'Server call', 1000);

		runs(function () {
			expect(retVal).toEqual([ {
				1: new Date('2010-12-01'),
				2: new Date('2010-12-02')
			}, {
				3: new Date('2010-12-03'),
				4: new Date('2010-12-04')
			}, {
				123: new Date('2010-12-23')
			} ]);
		});
	});
});