class Person(val firstName: String, val lastName: String, val position: String) {
	println("Creating " + toString())

	override def toString: String = s"$firstName $lastName holds $position position"
}