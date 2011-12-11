var connect = require('connect'),
	rpc = require('./rpc'),
	pg = require('pg'),
	dbCfg = {
		user: 'postgres',
		database: 'postgres',
		password: '',
		port: 5432,
		host: 'localhost'
	};

connect.createServer()
	.use(connect.favicon())
	.use(connect.logger())
	.use(connect.directory(__dirname))
	.use(connect.static(__dirname))
	.use(function (req, res, next) {
		pg.connect(dbCfg, function(err, client) {
			client.query("SELECT version();", function(err, result) {
				res.writeHead(200, {'Content-Type': 'text/plain'});
				res.end(JSON.stringify(result.rows));
			});
		});
	})
	.listen(8080);
