load('src/main/Make.js');
load('src/main/Make.Utils.js');
load('src/main/Make.Project.js');
load('src/main/Make.Task.js');
load('src/main/Make.RecursionChecker.js');
load('src/main/Make.AntPathMatcher.js');
load('src/main/Make.Sys.js');
load('src/main/Make.FsScanner.js');
load('src/main/Make.Main.js');

load('tools/JSLint-2011.05.10/jslint.js');

var main = new Make.Main();
main.initGlobalScope(this);

project('my project', 'compile', function () {
	var sys = Make.Sys; // This is like a Java "import" statement
	var utils = Make.Utils; // This is like a Java "import" statement

	task('jslint', [], function () {
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
		utils.each(jsFiles, function (file) {
			var content = sys.readFile(sys.combinePath('src', file));
			JSLINT(content, options);
			utils.each(JSLINT.errors, function (error) {
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

	task('compile', [ 'jslint' ], function () {
		var mainFiles = [
			'Make.js',
			'Make.Sys.js',
			'Make.Task.js',
			'Make.Project.js',
			'Make.AntPathMatcher.js',
			'Make.FsScanner.js',
			'Make.RecursionChecker.js',
			'Globals.js'
		];
		mainFiles = utils.map(mainFiles, function (file) {
			return sys.readFile(sys.combinePath('src/main', file));
		});
		sys.writeFile('build/jsmake.js', mainFiles.join('\n'));
	});
});

main.run(arguments);

