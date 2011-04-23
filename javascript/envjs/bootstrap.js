Packages.org.mozilla.javascript.Context.getCurrentContext().setOptimizationLevel(-1); // this is the same as passing -opt -1 to the commandline when launching rhino
load('env.rhino.1.2.js');
Envjs({
	scriptTypes: {
		'': true,
		'text/javascript': true
	},
	afterScriptLoad: {
        'qunit': function () {
/*
			console.log("QUnit loaded.");
			QUnit.testStart = function(params) {
				console.log("QUnit.testStart(name=%s)", params.name);
			};
			QUnit.log = function (params) {
				console.log("QUnit.log(result=%s, actual=%s, expected=%s, message=%s)", params.result, params.actual, params.expected, params.message);
			};
*/
			QUnit.done = function (params){
				console.log("QUnit.done(failed=%s, passed=%s, total=%s, runtime=%s)", params.failed, params.passed, params.total, params.runtime);
			};
		}
    }
});

window.location = 'file:///C:/Users/gianmarco.gherardi/Tools/envjs/testpage.html';
