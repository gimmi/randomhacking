Riferimenti
	http://www.kindleit.net/maven_gae_plugin/usage.html
	http://googlewebtoolkit.blogspot.com/2010/08/how-to-use-google-plugin-for-eclipse.html

mvn gae:unpack
	Per scaricare da maven central le dipendenze di GAE SDK necessarie ad eseguire i goals di maven-gae-plugin
mvn gae:run
	Per eseguire il dev server in locale
mvn gae:deploy
	Per eseguire il deploy della applicazione. Bisogna prima incrementare src\main\webapp\WEB-INF\appengine-web.xml\version 
