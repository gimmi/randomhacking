var connect = require('connect'),
	rpc = require('./rpc');

connect.createServer()
	.use(connect.favicon())
	.use(connect.logger())
	.use(connect.directory(__dirname))
	.use(connect.static(__dirname))
	.use(rpc.rpc())
	.listen(8080);
