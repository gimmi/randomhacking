package com.github.gimmi.spikescalaservlet

import scala.slick.driver.ExtendedProfile
import scala.slick.session.Database

trait SlickProfileComponent {
	val slickProfile: ExtendedProfile
	val database: Database
}
