describe("Make.FsScanner", function() {
	var target;

	beforeEach(function() {
		
	});

	it("should scan", function() {
		spyOn(Make.Sys, 'getFiles').andCallFake(function (path) {
			return {
				'base/.': [ 'file1', 'file2' ],
				'base/./fld1': [ 'file3' ]
			}[path];
		});
		spyOn(Make.Sys, 'getDirectories').andCallFake(function (path) {
			return {
				'base/.': [ 'fld1' ],
				'base/./fld1': [],
			}[path];
		});
		spyOn(Make.Sys, 'combinePath').andCallFake(function (path1, path2) {
			return [path1, path2].join('/');
		});
		
		var actual = new Make.FsScanner('base').scan();

		expect(actual).toEqual([ './file1', './file2', './fld1/file3' ]);
	});
});
