Ext.define('Spike.controller.Users', {
	extend: 'Ext.app.Controller',

	models: [ 'User' ],
	
	stores: [ 'Users' ],

	views: [
		'user.List',
		'user.Edit'
	],

	init: function () {
		this.control({
			'userlist': {
				itemdblclick: this.editUser
			}
		});
	},

	editUser: function (grid, record) {
		var view = Ext.widget('useredit');
		view.down('form').loadRecord(record);
	}
});