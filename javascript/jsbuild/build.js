load('tools/utils.js');
load('tools/JSLint-2011.05.10/jslint.js');

project('my project', 'default', function () {
	task('default', ['get latest'], function () {
		print('running');
	});
	
	task('jslint', [], function () {
		if(!JSLINT('', {})) {
			throw 'JSLint fail';
		}
	});
});

/*
Helpers.forEachFile('.', /\.js$/, function (file) {
	print(file);
});

print(Helpers.runCmd("command.bat"));
*/
