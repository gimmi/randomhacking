describe("Make.AntPathMatcher", function() {
	var target;

	beforeEach(function() {
		target = new Make.AntPathMatcher();
	});
	
	it('valid match without wildcards', function () {
		expect(target.fileMatch('a', 'a')).toBeTruthy();
		expect(target.fileMatch('a/b', 'a/b')).toBeTruthy();
	});
	
	it('invalid match without wildcards', function () {
		expect(target.fileMatch('a', 'b')).toBeFalsy();
		expect(target.fileMatch('a', 'a/b')).toBeFalsy();
		expect(target.fileMatch('a/b', 'a')).toBeFalsy();
		expect(target.fileMatch('a/b', 'a/b/c')).toBeFalsy();
	});
	
	it('should match pattern with ** wildcard', function () {
		expect(target.fileMatch('**', 'a')).toBeTruthy();
		expect(target.fileMatch('**', 'a/b')).toBeTruthy();
		expect(target.fileMatch('a/**', 'a/b')).toBeTruthy();
		expect(target.fileMatch('a/**', 'a/b/c')).toBeTruthy();
		expect(target.fileMatch('a/**/c', 'a/b')).toBeFalsy();
		expect(target.fileMatch('a/**/c', 'a/b/c')).toBeTruthy();
		expect(target.fileMatch('a/**/d', 'a/b/c/d')).toBeTruthy();
	});
	
	it('should detect if path can contain pattern matching elements', function () {
		expect(target.directoryMatch('**', 'a')).toBeTruthy();
		expect(target.directoryMatch('**', 'a/b')).toBeTruthy();
		expect(target.directoryMatch('a/**', 'a/b')).toBeTruthy();
		expect(target.directoryMatch('a/**', 'a/b/c')).toBeTruthy();
		expect(target.directoryMatch('a/**/c', 'a/b')).toBeFalsy();
		expect(target.directoryMatch('a/**/c', 'a/b/c')).toBeTruthy();
		expect(target.directoryMatch('a/**/d', 'a/b/c/d')).toBeTruthy();
	});
	
	it('should tokenize path/pattern', function () {
		expect(target._tokenize('a/b/c')).toEqual([ 'a', 'b', 'c' ]);
		expect(target._tokenize('a\\b\\c')).toEqual([ 'a', 'b', 'c' ]);
		expect(target._tokenize(' a / b / c ')).toEqual([ 'a', 'b', 'c' ]);
		expect(target._tokenize('/a')).toEqual([ 'a' ]);
		expect(target._tokenize(' / a')).toEqual([ 'a' ]);
		expect(target._tokenize('a/')).toEqual([ 'a' ]);
		expect(target._tokenize('a / ')).toEqual([ 'a' ]);
		expect(target._tokenize('a/./b')).toEqual([ 'a', 'b' ]);
	});
});