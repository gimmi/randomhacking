var express = require('express'),
	app = express.createServer();

app.configure(function () {
	app.set('views', __dirname + '/views');
	app.set('view engine', 'ejs');
	app.set('view options', { layout: false });

	app.use(express.logger());
	app.use(express.bodyParser());
	app.use(express.static(__dirname + '/public'));
});

app.configure('development', function() {
  app.use(express.errorHandler({ dumpExceptions: true, showStack: true }));
});

app.configure('production', function() {
  app.use(express.errorHandler());
});

app.get('/', function(req, res) {
   res.render('index', { content: 'EJS rendered view' });
});

app.listen(3000, function() {
	console.log("Express server listening on port %d in %s mode", app.address().port, app.settings.env);
});
