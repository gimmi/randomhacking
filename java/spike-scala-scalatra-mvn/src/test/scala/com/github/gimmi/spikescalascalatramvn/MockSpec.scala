package com.github.gimmi.spikescalascalatramvn

import org.specs2.mock.Mockito
import org.specs2.mutable._

class MockSpec extends Specification with Mockito {
	"Mocking an object" should {
		"not call methods" in {
			val m = mock[java.util.List[String]]
			m.get(0) returns "one"
			m.get(0) must be equalTo("one")
		}
	}
}