load('tools/utils.js');

project('my project', 'build', function () {
	task('build', ['get latest'], function () {
		print('running');
	});
	
	task('get latest', [], function () {
		print('running');
	});
});

/*
Helpers.forEachFile('.', /\.js$/, function (file) {
	print(file);
});

print(Helpers.runCmd("command.bat"));
*/
