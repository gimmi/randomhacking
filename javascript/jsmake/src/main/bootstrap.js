/*global Make */

(function (args) {
	var main = new Make.Main();
	this.project = function () {
		main.project.apply(main, arguments);
	};
	this.task = function () {
		main.task.apply(main, arguments);
	};
	Make.Sys.loadJavascriptFile('build.js');
	main.run(args);
}(arguments));