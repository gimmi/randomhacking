Packages.org.mozilla.javascript.Context.getCurrentContext().setOptimizationLevel(-1);
load("env.rhino.1.2.js");

load("jasmine.js");

load("examplespec.js");

jasmine.RhinoReporter = function() {
	this._results = "";
};
jasmine.RhinoReporter.prototype = {
	reportRunnerStarting: function(runner) {
	},
	reportRunnerResults: function(runner) {
		var failedCount = runner.results().failedCount;

		this.log(this._results);
		this.log("Passed: " + runner.results().passedCount);
		this.log("Failed: " + failedCount);
		this.log("Total : " + runner.results().totalCount);

		if (failedCount > 0) {
			java.lang.System.exit(1);
		}
	},
	reportSuiteResults: function(suite) {
	},
	reportSpecStarting: function(spec) {
	},
	reportSpecResults: function(spec) {
		var i, specResults = spec.results().getItems();

		if (spec.results().passed()) {
			java.lang.System.out.print(".");
		} else {
			java.lang.System.out.print("F");
			this._results += "FAILED\n";
			this._results += "Suite: " + spec.suite.description + "\n";
			this._results += "Spec : " + spec.description + "\n";
			for (i = 0; i < specResults.length; i += 1) {
				this._results += specResults[i].trace + "\n";
			}
		}
	},
	log: function(str) {
		print(str);
	}
};
jasmine.getEnv().addReporter(new jasmine.RhinoReporter());

window.location = 'file:///' + arguments[0] + "RhinoRunner.html";
