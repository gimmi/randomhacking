/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc */
"use strict";
Ext.namespace('ExtMvc');

ExtMvc.TerritoryColumn = Ext.extend(Ext.grid.Column, {
    constructor: function(cfg){
        ExtMvc.TerritoryColumn.superclass.constructor.call(this, cfg);
        this.renderer = function(value, metadata, record, rowIndex, colIndex, store) {
            return value.Description;
        };
    }
});

Ext.grid.Column.types['ExtMvc.TerritoryColumn'] = ExtMvc.TerritoryColumn;