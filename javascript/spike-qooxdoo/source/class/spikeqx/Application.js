/* ************************************************************************
#asset(spikeqx/*)
************************************************************************ */

qx.Class.define("spikeqx.Application", {
	extend : qx.application.Standalone,

	members : {
		main : function() {
			this.base(arguments);

			if (qx.core.Environment.get("qx.debug")) {
				qx.log.appender.Native;
				qx.log.appender.Console;
			}

			var button1 = new qx.ui.form.Button("First Button", "spikeqx/test.png");
			var doc = this.getRoot();

			doc.add(button1, {
				left : 100,
				top  : 50
			});

			button1.addListener("execute", function(e) {
				alert("Hello World!");
			});
		}
	}
});