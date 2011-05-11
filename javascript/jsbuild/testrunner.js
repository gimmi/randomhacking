Packages.org.mozilla.javascript.Context.getCurrentContext().setOptimizationLevel(-1); // this is the same as passing -opt -1 to the commandline when launching rhino

load('lib/test/jasmine-1.0.2/jasmine.js');

load('lib/main/underscore-1.1.6/underscore.js');
load('src/main/utils.js');

load('src/test/tests.js');

jasmine.RhinoReporter = function() {
};
jasmine.RhinoReporter.prototype.reportRunnerStarting = function(runner) {
	this.log("Runner Started.");
};
jasmine.RhinoReporter.prototype.reportRunnerResults = function(runner) {
	this.log("Runner Finished.");
};
jasmine.RhinoReporter.prototype.reportSuiteResults = function(suite) {
	var results = suite.results();
	this.log(suite.description + ": " + results.passedCount + " of " + results.totalCount + " passed.");
};
jasmine.RhinoReporter.prototype.reportSpecStarting = function(spec) {
	this.log(spec.suite.description + ' : ' + spec.description + ' ... ');
};
jasmine.RhinoReporter.prototype.reportSpecResults = function(spec) {
	this.log(spec.results().passed() ? "Passed." : "Failed.");
};
jasmine.RhinoReporter.prototype.log = function(str) {
	print(str);
};

jasmine.getEnv().addReporter(new jasmine.RhinoReporter());
jasmine.getEnv().execute();

