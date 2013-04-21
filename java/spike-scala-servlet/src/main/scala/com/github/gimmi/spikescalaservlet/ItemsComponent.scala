package com.github.gimmi.spikescalaservlet

trait ItemsComponent { this: SlickProfileComponent =>
	import slickProfile._

	object Items extends Table[(String, String)]("Items") {
		def id = column[String]("Id", O.PrimaryKey)
		def title = column[String]("Title")
		def * = id ~ title
	}
}
