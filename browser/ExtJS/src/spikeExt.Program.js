Ext.namespace('spikeExt');

spikeExt.Program = function() {
};

spikeExt.Program.prototype.newTab = function(btn) {
	this.tabPanel.add( {
		title : 'New Tab',
		closable : true,
		html : '<p>New Tab</p>'
	});
};

spikeExt.Program.prototype.showMsg = function(btn) {
	Ext.Msg.alert('Click', btn.text);
};

spikeExt.Program.prototype.openWindow = function(btn) {
	var window = new spikeExt.Window(false);
	window.show();
};

spikeExt.Program.prototype.openWindowModal = function(btn) {
	var window = new spikeExt.Window(true);
	window.show();
};

spikeExt.Program.prototype.main = function() {
	this.tabPanel = new Ext.TabPanel( {
		flex : 1,
		activeTab : 0,
		border : false,
		items : [ {
			title : 'Tab 1',
			html : '<p>Center</p>'
		}, {
			title : 'Tab 2',
			closable : true,
			html : '<p>Center2</p>'
		} ]
	});

	this.viewport = new Ext.Viewport( {
		layout : {
			type : 'vbox',
			pack : 'start',
			align : 'stretch'
		},
		items : [ {
			xtype : 'toolbar',
			items : [ {
				text : 'File',
				menu : {
					xtype : 'menu',
					items : [ {
						text : 'New Tab',
						handler : this.newTab,
						scope : this
					}, {
						text : 'Open Window',
						handler : this.openWindow,
						scope : this
					}, {
						text : 'Open Window Modal',
						handler : this.openWindowModal,
						scope : this
					}, {
						text : 'Save',
						handler : this.showMsg,
						scope : this
					}, '-', {
						text : 'Exit',
						handler : this.showMsg,
						scope : this
					} ]
				}
			}, {
				text : 'Help'
			} ]
		}, this.tabPanel ]
	});
};
