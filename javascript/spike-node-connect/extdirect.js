exports.ExtDirect = function (path, api) {
	this._path = path;
	this._api = api;
};
exports.ExtDirect.prototype = {
	handleRequest: function (req, res) {
		switch(req.method) {
		case 'GET':
			res.writeHead(200, { 'Content-Type': 'text/javascript' });
			res.end('Ext.ns("Ext.app");\nExt.app.REMOTING_API = ' + JSON.stringify(this._api, null, '\t') + ';');
			break;
		case 'POST':
			if(req.headers['Content-Type'] === 'application/json') {
				this._doJsonPost(req, res);
			} else {
				res.writeHead(415, { 'Content-Type': 'text/plain' });
				res.end('415 Unsupported Media Type');
			}
			break;
		default:
			res.writeHead(405, { 'Content-Type': 'text/plain' });
			res.end('405 Method Not Allowed');
		}
	},

	_doJsonPost: function (req, res) {
		var stringData = '',
			jsonData;
		req.on('data', function(chunk) {
			stringData += chunk.toString('UTF8');
		});
		req.on('end', function() {
			jsonData = JSON.parse(stringData);
		});
	}
};