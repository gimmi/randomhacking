/*jslint white: true, browser: true, devel: true, onevar: true, undef: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, strict: true, newcap: true, immed: true */
/*global Ext, Rpc, ExtMvc */
"use strict";

Ext.namespace('ExtMvc');

ExtMvc.EmployeeFormPanel = Ext.extend(Ext.form.FormPanel, {
	initComponent: function () {
		var _this = this;

		Ext.apply(_this, {
			border: false,
			padding: 10,
			items: [
				{ name: 'StringId', xtype: 'hidden' },
				{ name: 'EmployeeId', fieldLabel: 'EmployeeId', xtype: 'numberfield' },
				{ name: 'LastName', fieldLabel: 'LastName', xtype: 'textfield' },
				{ name: 'FirstName', fieldLabel: 'FirstName', xtype: 'textfield' },
				{ name: 'Title', fieldLabel: 'Title', xtype: 'textfield' },
				{ name: 'TitleOfCourtesy', fieldLabel: 'TitleOfCourtesy', xtype: 'textfield' },
				{ name: 'BirthDate', fieldLabel: 'BirthDate', xtype: 'datefield' },
				{ name: 'HireDate', fieldLabel: 'HireDate', xtype: 'datefield' },
				{ name: 'Address', fieldLabel: 'Address', xtype: 'textfield' },
				{ name: 'City', fieldLabel: 'City', xtype: 'textfield' },
				{ name: 'Region', fieldLabel: 'Region', xtype: 'textfield' },
				{ name: 'PostalCode', fieldLabel: 'PostalCode', xtype: 'textfield' },
				{ name: 'Country', fieldLabel: 'Country', xtype: 'textfield' },
				{ name: 'HomePhone', fieldLabel: 'HomePhone', xtype: 'textfield' },
				{ name: 'Extension', fieldLabel: 'Extension', xtype: 'textfield' },
				{ name: 'Notes', fieldLabel: 'Notes', xtype: 'textfield' },
				{ name: 'PhotoPath', fieldLabel: 'PhotoPath', xtype: 'textfield' }
			]
		});

		ExtMvc.EmployeeFormPanel.superclass.initComponent.apply(_this, arguments);
	}
});