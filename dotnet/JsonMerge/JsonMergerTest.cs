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

        [Test]
        public void Should_merge_equal_objects_without_id()
        {
            var first = @"{ a: 1, b: 2 }";

            var second = @"{ a: 1, b: 2 }";

            var actual = JsonMerger.Merge(first, second);

            Assert.That(actual, new JsonEqualConstraint(@"{ a: 1, b: 2 }"));
        }

        [Test]
        public void Should_use_id_field_for_identity()
        {
            var actual = JsonMerger.Merge("[{ id: 'a', a: 1, b: 2 }]", "[{ id: 'a', a: 1, b: 2 }]");
            Assert.That(actual, new JsonEqualConstraint("[{ id: 'a', a: 1, b: 2 }]"));

            actual = JsonMerger.Merge("[{ Id: 'a', a: 1, b: 2 }]", "[{ Id: 'a', a: 1, b: 2 }]");
            Assert.That(actual, new JsonEqualConstraint("[{ Id: 'a', a: 1, b: 2 }]"));

            actual = JsonMerger.Merge("[{ id: [1, 2], a: 1, b: 2 }]", "[{ id: [1, 2], a: 1, b: 2 }]");
            Assert.That(actual, new JsonEqualConstraint("[{ id: [1, 2], a: 1, b: 2 }]"));

            actual = JsonMerger.Merge("[{ $id: 'a', a: 1, b: 2 }]", "[{ $id: 'a', a: 1, b: 2 }]");
            Assert.That(actual, new JsonEqualConstraint("[{ $id: 'a', a: 1, b: 2 }]"));

            actual = JsonMerger.Merge("[{ $id: 'a', id: 'x', a: 1, b: 2 }]", "[{ $id: 'a', id: 'y', a: 1, b: 2 }]");
            Assert.That(actual, new JsonEqualConstraint("[{ $id: 'a', id: 'x', a: 1, b: 2 }]"));

            actual = JsonMerger.Merge("[{ $id: 'x', id: 'a' }]", "[{ $id: 'y', id: 'a' }]");
            Assert.That(actual, new JsonEqualConstraint("[{ $id: 'x', id: 'a' }, { $id: 'y', id: 'a' }]"));
        }
    }
}
