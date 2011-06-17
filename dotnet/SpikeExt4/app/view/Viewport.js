Ext.define('Spike.view.Viewport', {
	extend: 'Ext.container.Viewport',
	layout: 'fit',
	initComponent: function () {
		this.items = [ {
			xtype: 'tabpanel',
			items: {
				title: 'Tasks',
				xtype: 'taskinfo.List'
			}
		} ];

		this.callParent(arguments);
	}
});
