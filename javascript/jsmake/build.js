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
		var options = {
			white: true,
			onevar: true,
			undef: true,
			regexp: true,
			plusplus: true,
			bitwise: true,
			newcap: true
		};
		var jsFiles = new Make.FsScanner('src').include('**/*.js').scan();
		var errors = [];
		Make.each(jsFiles, function (file) {
			var content = sys.readFile(sys.combinePath('src', file));
			JSLINT(content, options);
			Make.each(JSLINT.errors, function (error) {
				if (error) {
					errors.push(file + ':' + error.line + ',' + error.character + ': ' + error.reason);
				}
			});
		});

		if (errors.length) {
			sys.log('JSLint found ' + errors.length + ' errors');
			sys.log(errors.join('\n'));
			throw 'Fatal error, see previous messages.';
		}
	});

	task('files', [], function () {
		sys.log('running files task');
		sys.createDirectory('./build');
		sys.deletePath('a');
		var scanner = new Make.FsScanner('src').include('**/*.js');
		Make.each(scanner.scan(), function (file) {
			sys.log(file);
		});
	});
});
