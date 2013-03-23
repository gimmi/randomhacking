package com.github.gimmi.spikescalascalatramvn

import org.scalatra._
import org.scalatra.json._
import org.slf4j.LoggerFactory

class ItemServlet extends ScalatraServlet with JacksonJsonSupport {

	val logger =  LoggerFactory.getLogger(getClass)

	protected implicit val jsonFormats: org.json4s.Formats = org.json4s.DefaultFormats

	before() {
		contentType = formats("json")
	}

	get("/items") {
		logger.info("call")
		Ok(s"You asked for all items")
	}

	get("/items/:id") {
		logger.info("call")
		Ok(s"You asked for ${params('id)}")
	}

	delete("/items/:id") {
		logger.info("call")
		Ok(s"You asked to delete ${params('id)}")
	}

	post("/items") {
		logger.info("call")
		Ok(s"You asked to create/modify")
	}
}
