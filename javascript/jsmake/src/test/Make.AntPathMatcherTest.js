describe("Make.AntPathMatcher", function() {
	var target;
	beforeEach(function() {
		target = new Make.AntPathMatcher();
	});
	
	it('should match', function () {
		expect(target.match('a', 'a')).toBeTruthy();
		expect(target.match('a/b', 'a/b')).toBeTruthy();
		expect(target.match('a/b', 'a/b/c')).toBeTruthy();
	});
	
	it('should match pattern with ** wildcard', function () {
		expect(target.match('**', 'a')).toBeTruthy();
		expect(target.match('**', 'a/b')).toBeTruthy();
		expect(target.match('a/**', 'a/b')).toBeTruthy();
		expect(target.match('a/**', 'a/b/c')).toBeTruthy();
		expect(target.match('a/**/c', 'a/b')).toBeFalsy();
		expect(target.match('a/**/c', 'a/b/c')).toBeTruthy();
		expect(target.match('a/**/d', 'a/b/c/d')).toBeTruthy();
	});
	
	it('should normalize pattern', function () {
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