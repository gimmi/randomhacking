package com.github.gimmi.spikescalascalatramvn

import org.scalatra.test.specs2._

class ItemServletSpec extends MutableScalatraSpec {
	addServlet(classOf[ItemServlet], "/*")

	"GET / on ItemServlet" should {
		"return status 200" in {
			get("/") {
				status must be equalTo(200)
			}
		}
	}
}
