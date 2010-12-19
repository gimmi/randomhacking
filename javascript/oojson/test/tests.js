/*global describe, beforeEach, it, expect */

"use strict";

describe('oojson', function () {
	var target, factory;

	beforeEach(function () {
		factory = jasmine.createSpyObj('factory1', [ 'canCreate', 'create' ]);
		target = new oojson.OoJson(JSON, [ factory ]);
	});

	it('Should normally parse json', function () {
		var json = target.parse('{"stringValue":"hi","numberValue":3.14,"boolValue":true,"arrayValue":[1,2,3]}');
		expect(json).toEqual({
			stringValue: 'hi',
			numberValue: 3.14,
			boolValue: true,
			arrayValue: [ 1, 2, 3 ]
		});
	});

	it('Should evaluate all factories', function () {
		factory.canCreate.andCallFake(function () {
			expect(arguments).toEqual([ '', {} ]);
			return false;
		});

		var json = target.parse('{}');

		expect(factory.canCreate.callCount).toEqual(1);
		expect(factory.create.callCount).toEqual(0);
	});

	it('Should call create if can create', function () {
		var newInstance = {};

		factory.canCreate.andCallFake(function () {
			return true;
		});
		factory.create.andCallFake(function (k, v) {
			expect(arguments).toEqual([ '', {} ]);
			return newInstance;
		});

		var actual = target.parse('{}');

		expect(actual).toBe(newInstance);
	});

	it('Should apply passed reviver after factories', function () {
		var newInstance = {};
		factory.canCreate.andCallFake(function () {
			return true;
		});
		factory.create.andCallFake(function () {
			return newInstance;
		});
		var actual = target.parse('{}', function (k, v) {
			expect(k).toEqual('');
			expect(v).toBe(newInstance);
			return {
				wrapped: v
			};
		});

		expect(actual.wrapped).toBe(newInstance);
	});
});