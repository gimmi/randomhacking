/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, ExtMvc.Ns */
"use strict";

Ext.namespace("ExtMvc.Ns");

ExtMvc.Ns.EmployeeGridPanel = Ext.extend(Ext.grid.GridPanel, {
	border: false,
	initComponent: function () {
		this.colModel = new Ext.grid.ColumnModel({
			defaults: { width: 60, sortable: true },
			columns: [
				{ dataIndex: 'EmployeeId', header: 'EmployeeId', xtype: 'numbercolumn' }, { dataIndex: 'LastName', header: 'LastName', xtype: 'gridcolumn' }, { dataIndex: 'FirstName', header: 'FirstName', xtype: 'gridcolumn' }, { dataIndex: 'Title', header: 'Title', xtype: 'gridcolumn' }, { dataIndex: 'TitleOfCourtesy', header: 'TitleOfCourtesy', xtype: 'gridcolumn' }, { dataIndex: 'BirthDate', header: 'BirthDate', xtype: 'datecolumn' }, { dataIndex: 'HireDate', header: 'HireDate', xtype: 'datecolumn' }, { dataIndex: 'Address', header: 'Address', xtype: 'gridcolumn' }, { dataIndex: 'City', header: 'City', xtype: 'gridcolumn' }, { dataIndex: 'Region', header: 'Region', xtype: 'gridcolumn' }, { dataIndex: 'PostalCode', header: 'PostalCode', xtype: 'gridcolumn' }, { dataIndex: 'Country', header: 'Country', xtype: 'gridcolumn' }, { dataIndex: 'HomePhone', header: 'HomePhone', xtype: 'gridcolumn' }, { dataIndex: 'Extension', header: 'Extension', xtype: 'gridcolumn' }
			]
		});
		ExtMvc.Ns.EmployeeGridPanel.superclass.initComponent.apply(this, arguments);
	}
});