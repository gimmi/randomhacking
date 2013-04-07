package com.github.gimmi.spikescalaservlet

import javax.servlet.http.{HttpServletResponse, HttpServletRequest, HttpServlet}
import com.fasterxml.jackson.module.scala.DefaultScalaModule
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.core.JsonParser

class HelloWorldServlet extends HttpServlet {

  val mapper = new ObjectMapper()
    .registerModule(DefaultScalaModule)
    .configure(JsonParser.Feature.ALLOW_SINGLE_QUOTES, true)
    .configure(JsonParser.Feature.ALLOW_UNQUOTED_FIELD_NAMES, true)


  override def doGet(req: HttpServletRequest, resp: HttpServletResponse) {
    val encoding = "UTF-8"

    resp setStatus HttpServletResponse.SC_OK
    resp setCharacterEncoding encoding
    resp setContentType s"application/json; charset=$encoding"

    val tree = mapper.readTree("{ a: 123, b: [4, 5, 6] }")

    val json = Map(
      "ok" -> true,
      "a" -> "B",
      "c" -> List(1, 2, 3)
    )

    resp.getWriter.write(mapper.writeValueAsString(json))
  }
}
