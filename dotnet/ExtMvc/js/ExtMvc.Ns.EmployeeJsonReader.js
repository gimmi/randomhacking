/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc.Ns */
"use strict";

Ext.namespace('ExtMvc.Ns');

ExtMvc.Ns.EmployeeJsonReader = Ext.extend(Rpc.JsonReader, {
	constructor: function (meta, recordType) {
		var cfg = {
			root: 'items',
			idProperty: 'StringId',
			totalProperty: 'count',
			fields: [
				'StringId', 
				'EmployeeId',
				'LastName',
				'FirstName',
				'Title',
				'TitleOfCourtesy',
				'BirthDate',
				'HireDate',
				'Address',
				'City',
				'Region',
				'PostalCode',
				'Country',
				'HomePhone',
				'Extension',
				'Notes',
				'PhotoPath'
			]
		};
		ExtMvc.Ns.EmployeeJsonReader.superclass.constructor.call(this, Ext.apply(meta || {}, cfg), recordType);
	}
});