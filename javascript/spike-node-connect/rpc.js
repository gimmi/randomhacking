exports.rpc = function (path, options) {
	var options = options || {},
		path = path || __dirname + '/rpc';

	return function favicon(req, res, next) {
		console.log('here');
		next();
	};
};