package com.github.gimmi.spikescalaservlet

import scala.slick.session.Database

import scala.slick.driver.H2Driver
import scala.slick.driver.H2Driver.simple._
import org.scalatest._

import Database.threadLocalSession

class DaoSpec extends FlatSpec {
	"Tables" should "return true" in {
		object app extends SlickProfileComponent with ItemsComponent {
			val slickProfile = H2Driver
			val database = Database.forURL("jdbc:h2:mem:test;DB_CLOSE_DELAY=-1;MVCC=TRUE", driver = "org.h2.Driver")
		}

		app.database withSession {
			app.Items.ddl.create

			app.Items.insertAll(
				("id1", "title1"),
				("id2", "title2")
			)

			Query(app.Items) foreach { case (id, title) => println(s"#$id - $title") }
		}

		assert(true === true)
	}
}
