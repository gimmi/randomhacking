/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc.Ns */
"use strict";

Ext.namespace('ExtMvc.Ns');

ExtMvc.Ns.CustomerDemographicJsonReader = Ext.extend(Rpc.JsonReader, {
	constructor: function (meta, recordType) {
		var cfg = {
			root: 'items',
			idProperty: 'StringId',
			totalProperty: 'count',
			fields: [
				'StringId', 
				{
					name: '$ref',
					convert: function (v, r) {
						return r;
					}
				},
				'CustomerTypeId',
				'CustomerDesc'
			]
		};
		ExtMvc.Ns.CustomerDemographicJsonReader.superclass.constructor.call(this, Ext.apply(meta || {}, cfg), recordType);
	}
});