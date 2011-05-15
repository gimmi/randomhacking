describe("Make.AntPathMatcher", function() {
	var target;
	beforeEach(function() {
		target = new Make.AntPathMatcher();
	});
	
	it('should normalize pattern', function () {
		expect(target._normalize('a/b/c')).toEqual('a/b/c');
		expect(target._normalize('a\\b\\c')).toEqual('a/b/c');
		expect(target._normalize(' a / b / c ')).toEqual('a/b/c');
		expect(target._normalize('/a')).toEqual('a');
		expect(target._normalize(' / a')).toEqual('a');
		expect(target._normalize('a/')).toEqual('a');
		expect(target._normalize('a / ')).toEqual('a');
		expect(target._normalize('a/./b')).toEqual('a/b');
	});
	
	xit("Should parse file patterns", function() {
		expect(target._parsePattern('file.txt')).toEqual([ 'file.txt' ]);
		expect(target._parsePattern('*.txt')).toEqual([ '*.txt' ]);
		expect(target._parsePattern('file?.txt')).toEqual([ 'file?.txt' ]);
		expect(target._parsePattern('*.*')).toEqual([ '*.*' ]);
		expect(target._parsePattern('**')).toEqual([ '**' ]);
		expect(target._parsePattern(' file with spaces.txt ')).toEqual([ 'file with spaces.txt' ]);
	});
	
	xit('should throw error with invalid pattern', function () {
		expect(function () { target._parsePattern('/file.txt'); }).toThrow('Absolute path patterns not supported');
		expect(function () { target._parsePattern('\\file.txt'); }).toThrow('Absolute path patterns not supported');
		expect(function () { target._parsePattern('\\\\file.txt'); }).toThrow('Absolute path patterns not supported');
	});
	
	xit("Should parse path patterns", function() {
		expect(target._parsePattern('a/b/c')).toEqual([ 'a', 'b', 'c' ]);
		expect(target._parsePattern('a\\b\\c')).toEqual([ 'a', 'b', 'c' ]);
		expect(target._parsePattern('a/**/b?')).toEqual([ 'a', '**', 'b?' ]);
	});
});