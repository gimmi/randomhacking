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

project('jsmake', 'compile', function () {
	var sys = Make.Sys; // This is like a Java "import" statement
	var utils = Make.Utils; // This is like a Java "import" statement

	task('jslint', [], function () {
		var files = new Make.FsScanner('src').include('**/*.js').scan();
		var errors = [];
		utils.each(files, function (file) {
			var content = '/*global Make: true, java, toString */\n' + sys.readFile(sys.combinePath('src', file));
			JSLINT(content, { white: true, onevar: true, undef: true, regexp: true, plusplus: true, bitwise: true, newcap: true, rhino: true });
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
		var mainFiles = new Make.FsScanner('src/main')
				.include('**/*.js')
				.exclude('Make.js')
				.exclude('bootstrap.js')
				.scan();
		mainFiles.unshift('Make.js'); // must be on top
		mainFiles.push('bootstrap.js'); // must be on bottom
		mainFiles = utils.map(mainFiles, function (file) {
			return sys.readFile(sys.combinePath('src/main', file));
		});

		var header = [];
		header.push('/*');
		header.push('JSMake version ' + sys.readFile('VERSION'));
		header.push('');
		header.push(sys.readFile('LICENSE'));
		header.push('*/');
		mainFiles.unshift(header.join('\n'));

		sys.writeFile('build/jsmake.js', mainFiles.join('\n'));
	});
});

main.run(arguments);

