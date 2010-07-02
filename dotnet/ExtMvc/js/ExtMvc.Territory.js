/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.Territory = {
	toString: function (o) {
		if (o) {
			return o.Description || o.StringId || '[some value]';
		}
		return '';
	}
};