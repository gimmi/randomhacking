qx.Class.define("spikeqx.MainWindow", {
	extend : qx.ui.window.Window,

	events : {
		"reload" : "qx.event.type.Event",
		"post"   : "qx.event.type.Data"
	},

	construct : function() {
		this.base(arguments, "twitter", "spikeqx/t_small-c.png");
		this.setShowClose(false);
		this.setShowMaximize(false);
		this.setShowMinimize(false);
		this.setWidth(250);
		this.setHeight(300);
		this.setContentPadding(0);

		var layout = new qx.ui.layout.Grid(0, 0);
		layout.setRowFlex(1, 1);
		layout.setColumnFlex(0, 1);
		this.setLayout(layout);

		var toolbar = new qx.ui.toolbar.ToolBar();
		var reloadButton = new qx.ui.toolbar.Button("Reload");

		toolbar.add(reloadButton);

		this.add(toolbar, {
			row     : 0,
			column  : 0,
			colSpan : 2
		});

		var list = new qx.ui.form.List();

		this.add(list, {
			row     : 1,
			column  : 0,
			colSpan : 2
		});

		var textarea = new qx.ui.form.TextArea();
		textarea.setPlaceholder("Enter your message here...");
		this.add(textarea, {
			row    : 2,
			column : 0
		});

		var postButton = new qx.ui.form.Button("Post");
		postButton.setWidth(60);
		postButton.setEnabled(false);

		this.add(postButton, {
			row    : 2,
			column : 1
		});

		reloadButton.addListener("execute", function() {
			this.fireEvent("reload");
		}, this);

		textarea.addListener("input", function(e) {
			var value = e.getData();
			postButton.setEnabled(value.length < 140 && value.length > 0);
		}, this);

		postButton.addListener("execute", function() {
			this.fireDataEvent("post", textarea.getValue());
		}, this);
	}
});