/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.TerritoryJsonReader = Ext.extend(Rpc.JsonReader, {
	constructor: function (meta, recordType) {
		var cfg = {
			root: 'items',
			idProperty: 'StringId',
			totalProperty: 'count',
			fields: ['StringId', 'TerritoryId', 'TerritoryDescription', 'Region']
		};
		ExtMvc.TerritoryJsonReader.superclass.constructor.call(this, Ext.apply(meta || {}, cfg), recordType);
	}
});