var url = require('url');

exports.rpc = function (path, api) {
	return function (req, res, next) {
		if(url.parse(req.url).pathname !== path) {
			next();
		} else if(req.method === 'GET') {
			res.writeHead(200, { 'Content-Type': 'text/javascript' });
			res.end('Ext.ns("Ext.app");\nExt.app.REMOTING_API = ' + JSON.stringify(api, null, '\t') + ';');
		} else if(req.method === 'POST') {
			console.log('here');
		} else {
			res.writeHead(405, { 'Content-Type': 'text/plain' });
			res.end('405 Method Not Allowed');
		}
	};
};