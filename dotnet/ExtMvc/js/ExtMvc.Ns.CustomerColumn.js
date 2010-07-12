/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc.Ns */
"use strict";
Ext.namespace('ExtMvc.Ns');

ExtMvc.Ns.CustomerColumn = Ext.extend(Ext.grid.Column, {
    constructor: function(cfg){
        ExtMvc.Ns.CustomerColumn.superclass.constructor.apply(this, arguments);
        this.renderer = function(value, metadata, record, rowIndex, colIndex, store) {
            return value.Description;
        };
    }
});

Ext.grid.Column.types['ExtMvc.Ns.CustomerColumn'] = ExtMvc.Ns.CustomerColumn;