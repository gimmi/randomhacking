package com.github.gimmi.spikegroovyservlet

import groovy.json.JsonBuilder

import javax.servlet.http.HttpServlet
import javax.servlet.http.HttpServletRequest
import javax.servlet.http.HttpServletResponse

class SimpleGroovyServlet extends HttpServlet {
	void doGet(HttpServletRequest req, HttpServletResponse resp) {
		json(resp, [ok: true])
	}

	def json(HttpServletResponse resp, content) {
		resp.status = HttpServletResponse.SC_OK
		resp.characterEncoding = 'UTF-8'
		resp.contentType = "application/json; charset=${resp.characterEncoding}"

		new JsonBuilder(content).writeTo resp.writer
	}
}
