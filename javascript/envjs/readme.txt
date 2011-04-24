env.rhino.1.2.js: scaricato dal sito EnvJS
js.jar: Rhino javascript engine
qunit.css, qunit.js: scaricati dal sito di JQuery

è presente un bug in EnvJS che non permette a QUnit di funzionare. QUnit è stato modificato sostituendo tutti i "pre>" con "code>"
	http://envjs.lighthouseapp.com/projects/21590-envjs/tickets/176
	https://groups.google.com/d/topic/envjs/FN-GtdLiq-M/discussion
	
per lanciare i test eseguire i seguenti comandi
js launchtestpage.js
