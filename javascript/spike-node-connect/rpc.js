var url = require('url'),
	ExtDirect = require('./extdirect').ExtDirect;

exports.rpc = function (path, api) {
	var extDirect = new ExtDirect(path, api);
	return function (req, res, next) {
		var parsedUrl = url.parse(req.url);
		if (parsedUrl.pathname === path) {
			extDirect.handleRequest(req, res);
		} else {
			next();
		}
	};
};