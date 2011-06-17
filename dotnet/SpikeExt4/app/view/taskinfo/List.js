Ext.define('Spike.view.taskinfo.List', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.taskinfo.List',

	initComponent: function () {
		this.store = Ext.create('Spike.store.TaskInfos');
		
		this.columns = [
            { header: 'Title', dataIndex: 'title' }
        ];

		this.callParent(arguments);
	}
});