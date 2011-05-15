load('src/main/Globals.js');
load('src/main/Make.js');
load('src/main/Make.Project.js');
load('src/main/Make.Task.js');
load('src/main/Make.RecursionChecker.js');
load('src/main/Make.AntPathMatcher.js');
load('src/main/Make.Sys.js');

load('tools/JSLint-2011.05.10/jslint.js');

project('my project', 'default', function () {
	var sys = Make.Sys;
	
	task('default', ['jslint', 'files'], function () {
		sys.log('running default task');
	});
	
	task('jslint', [], function () {
		sys.log('running jslint task');
	});
	
	task('files', [], function () {
		sys.log('running files task');
		Make.each(Make.Sys.getFiles('.'), function (file) {
			sys.log(file);
		});
	});
});