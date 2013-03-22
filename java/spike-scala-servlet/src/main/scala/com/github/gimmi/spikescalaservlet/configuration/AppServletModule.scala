package com.github.gimmi.spikescalaservlet.configuration

import com.google.inject.servlet.ServletModule
import com.github.gimmi.spikescalaservlet.HelloServlet

class AppServletModule extends ServletModule {
	 override def configureServlets() {
		 serve("/hello").`with`(classOf[HelloServlet])
	 }
 }
