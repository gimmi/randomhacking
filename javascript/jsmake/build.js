load('src/main/Globals.js');
load('src/main/Make.js');
load('src/main/Make.Project.js');
load('src/main/Make.Task.js');
load('src/main/Make.RecursionChecker.js');
load('src/main/Make.AntPathMatcher.js');
load('src/main/Make.Sys.js');
load('src/main/Make.FsScanner.js');

load('tools/JSLint-2011.05.10/jslint.js');

project('my project', 'default', function () {
	var sys = Make.Sys; // This is like a Java "import" statement
	
	task('default', ['jslint', 'files'], function () {
		sys.log('running default task');
	});
	
	task('jslint', [], function () {
		sys.log('running jslint task');
		var jsFiles = new Make.FsScanner('src').include('**/*.js').scan();
		Make.each(jsFiles, function (file) {
			file = sys.combinePath('src', file);
			if (!JSLINT(content, {})) {
				Make.each(JSLINT.errors, function (error) {
					sys.log(file + ':' + error.line + ',' + error.character + ': ' + error.reason);
				}, this);
			}
			sys.log(content);
		});
	});
	
	task('files', [], function () {
		sys.log('running files task');
		var scanner = new Make.FsScanner('src').include('**/*.js');
		Make.each(scanner.scan(), function (file) {
			sys.log(file);
		});
	});
});
