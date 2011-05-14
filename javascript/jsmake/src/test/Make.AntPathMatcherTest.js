describe("Make.AntPathMatcher", function() {
	var target;
	beforeEach(function() {
		target = new Make.AntPathMatcher();
	});
	
	it("Should parse file patterns", function() {
		expect(target._parsePattern('file.txt')).toEqual([ 'file.txt' ]);
		expect(target._parsePattern('*.txt')).toEqual([ '*.txt' ]);
		expect(target._parsePattern('file?.txt')).toEqual([ 'file?.txt' ]);
		expect(target._parsePattern('*.*')).toEqual([ '*.*' ]);
		expect(target._parsePattern('**')).toEqual([ '**' ]);
		expect(target._parsePattern(' file with spaces.txt ')).toEqual([ 'file with spaces.txt' ]);
	});
	
	it('should throw error with invalid pattern', function () {
		expect(function () { target._parsePattern('/file.txt'); }).toThrow('Absolute path patterns not supported');
		expect(function () { target._parsePattern('\\file.txt'); }).toThrow('Absolute path patterns not supported');
		expect(function () { target._parsePattern('\\\\file.txt'); }).toThrow('Absolute path patterns not supported');
	});
	
	it("Should parse path patterns", function() {
		expect(target._parsePattern('a/b/c')).toEqual([ 'a', 'b', 'c' ]);
		expect(target._parsePattern('a\\b\\c')).toEqual([ 'a', 'b', 'c' ]);
		expect(target._parsePattern('a/**/b?')).toEqual([ 'a', '**', 'b?' ]);
	});
});