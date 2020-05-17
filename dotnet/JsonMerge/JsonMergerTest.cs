using NUnit.Framework;

namespace JsonMerge
{
    public class JsonMergerTest
    {
        [Test]
        public void Should_merge()
        {
            var first = @"{
                a: 1,
                b: null,
                raw_ary: ['a', 'b', 3],
                obj: {
                    a: 1,
                    b: 2,
                    c: 3
                },
                obj_ary: [{
                    $id: '1',
                    a: 1,
                    b: 2
                }, {
                    $id: '2',
                    a: 3,
                    b: 4
                }]
            }";

            var second = @"{
                a: 3,
                b: 2,
                raw_ary: ['a', 4, 'b', 3],
                obj: {
                    a: 1,
                    b: 'xoxo',
                    d: 4
                },
                obj_ary: [{
                    $id: '1',
                    a: 'xoxo',
                    b: 'xoxo',
                    c: 'lala'
                }, {
                    $id: '4',
                    a: 'ca',
                    b: 'so'
                }]
            }";

            var actual = JsonMerger.Merge(first, second);

            Assert.That(actual, new JsonEqualConstraint(@"{
                a: 1,
                b: 2,
                raw_ary: [ 'a', 'b', 3, 4 ],
                obj: {
                    a: 1,
                    b: 2,
                    c: 3,
                    d: 4
                },
                obj_ary: [{
                    $id: '1',
                    a: 1,
                    b: 2,
                    c: 'lala'
                }, {
                    $id: '2',
                    a: 3,
                    b: 4
                }, {
                    $id: '4',
                    a: 'ca',
                    b: 'so'
                }]
            }"));
        }

    }
}
