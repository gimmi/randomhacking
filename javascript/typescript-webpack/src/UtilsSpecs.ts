import * as Utils from './Utils';

describe('Utils', function() {
    it('should sum values', function() {
        expect(Utils.sum(1, 2)).toEqual(3);
    });

    it('should echo', function() {
        expect(Utils.echo('Hello world')).toEqual('Hello world');
    });
});
