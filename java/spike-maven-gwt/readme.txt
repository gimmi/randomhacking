See docs on http://code.google.com/intl/en/eclipse/docs/faq.html#gwt_with_maven

mvn ecipse:eclipse
Ensure that the version of GWT configured in Eclipse is the same as configured in POM
Eclipse validation error: The project 'spike-maven-gwt' does not have any GWT SDKs on its build path
	By default GPE consider this as an error, can be configured as warning:
	Window => Preferences => Google => Errors/Warnings => Project structure and SDKs => Missing SDK => Warning