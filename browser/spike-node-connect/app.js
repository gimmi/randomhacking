var connect = require('connect'),
	rpc = require('./rpc'),
	pg = require('pg');

connect.createServer()
	.use(connect.favicon())
	.use(connect.logger())
	.use(rpc.rpc('/rpc', {
		id: 'id',
		actions: {
			'Action1': [{
				name: 'method1',
				len: 1,
				fn: function (val) { return val; }
			}]
		}
	}))
	.use(connect.directory(__dirname))
	.use(connect.static(__dirname))
	.use(function (req, res, next) {
		pg.connect({ user: 'postgres', database: 'postgres', password: '', port: 5432, host: 'localhost' }, function(err, client) {
			client.query("SELECT version();", function(err, result) {
				res.writeHead(200, {'Content-Type': 'text/plain'});
				res.end([
					require('url').parse(req.url).pathname,
					JSON.stringify(result.rows)
				].join('\n'));
			});
		});
	})
	.listen(8080);
