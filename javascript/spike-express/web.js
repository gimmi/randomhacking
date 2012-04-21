var express = require('express'),
	app = express.createServer();

app.configure(function () {
	app.use(express.logger());
	app.use(express.static(__dirname + '/public'));
});

app.get('/', function(req, res){
  res.send('hello world');
});

var port = process.env.PORT || 3000;
app.listen(3000, function() {
	console.log("Listening on " + port);
});
