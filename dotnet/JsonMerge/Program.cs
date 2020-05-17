using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonMerge
{
    public class Program
    {
        public static void Main()
        {
            var firstToken = JToken.Parse(@"{
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
            }");
            var secondToken = JToken.Parse(@"{
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
            }");

            var resultToken = JsonMerger.Merge(firstToken, secondToken);

            var resultJson = resultToken.ToString(Formatting.Indented);

            Console.WriteLine(resultJson);
        }
    }
}
