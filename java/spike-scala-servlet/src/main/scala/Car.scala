class Car(val year: Int) {
	private var milesDriven: Int = 0
	def miles() = milesDriven
	def drive(distance: Int) {
		milesDriven += Math.abs(distance)
	}
}