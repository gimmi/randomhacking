/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.OrderJsonReader = Ext.extend(Rpc.JsonReader, {
	constructor: function (meta, recordType) {
		var cfg = {
			root: 'items',
			idProperty: 'StringId',
			totalProperty: 'count',
			fields: [
				'StringId', 
				'OrderId',
				'OrderDate',
				'RequiredDate',
				'ShippedDate',
				'Freight',
				'ShipName',
				'ShipAddress',
				'ShipCity',
				'ShipRegion',
				'ShipPostalCode',
				'ShipCountry',
				'Customer',
				'Employee'
			]
		};
		ExtMvc.OrderJsonReader.superclass.constructor.call(this, Ext.apply(meta || {}, cfg), recordType);
	}
});