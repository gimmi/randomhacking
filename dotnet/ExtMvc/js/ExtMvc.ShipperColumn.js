/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.ShipperColumn = Ext.extend(Ext.grid.Column, {
    constructor: function(cfg){
        ExtMvc.ShipperColumn.superclass.constructor.apply(this, arguments);
        this.renderer = function(value, metadata, record, rowIndex, colIndex, store) {
            return value.Description;
        };
    }
});

Ext.grid.Column.types['ExtMvc.ShipperColumn'] = ExtMvc.ShipperColumn;