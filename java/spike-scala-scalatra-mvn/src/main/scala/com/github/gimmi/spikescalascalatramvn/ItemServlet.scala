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
		logger.debug("Getting all items")
		List(Item("1", "Title 1"), Item("2", "Title 2"))
	}

	get("/items/:id") {
		logger.debug(s"Getting item #${params('id)}")
		Item("1", "Title 1")
	}

	delete("/items/:id") {
		logger.info("call")
		Ok(s"You asked to delete ${params('id)}")
	}

	post("/items") {
		val item = parsedBody.extract[Item]
		logger.debug(s"Should create/update item #${item.id}")
		Ok(s"You asked to create/modify")
	}
}
