package com.github.gimmi.spikescalaservlet

import javax.servlet.http.{HttpServletResponse, HttpServletRequest, HttpServlet}
import com.google.inject.Singleton

@Singleton
class HelloServlet extends HttpServlet {
	override def doGet(req: HttpServletRequest, resp: HttpServletResponse) {
		val encoding = "UTF-8"

		resp setStatus HttpServletResponse.SC_OK
		resp setCharacterEncoding encoding
		resp setContentType s"application/json; charset=$encoding"
		resp.getWriter.write("{ ok: true }")
	}
}
