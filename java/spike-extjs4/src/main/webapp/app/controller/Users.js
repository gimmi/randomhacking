Ext.define('AM.controller.Users', {
	extend: 'Ext.app.Controller',
	views: [ 'user.List', 'user.Edit' ],

	init: function() {
		this.control({
			'userlist': {
				itemdblclick: this.editUser
			}
		});
	},

	editUser: function (grid, record) {
		var view = Ext.create('AM.view.user.Edit');
		view.down('form').loadRecord(record);
	}
});
