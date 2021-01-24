module("Example module");

test("succesful test", function() {
	ok( true, "succesful assertion" );
});

test("failing test", function() {
	ok( false, "failing assertion" );
});
