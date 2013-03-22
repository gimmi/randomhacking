package com.github.gimmi.spikescalaservlet.configuration

import com.google.inject.servlet.GuiceServletContextListener
import com.google.inject.Guice

class AppGuiceServletContextListener extends GuiceServletContextListener {
	def getInjector = Guice.createInjector(new AppServletModule)
}

