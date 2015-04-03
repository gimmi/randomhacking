using System;
using System.IO;
using NUnit.Framework;

namespace JsonParser
{
    public class JsonParserTest
    {
        [Test]
        public void Tt()
        {
            var json = @"{
                'strProp': 'val',
                'numProp': -3.14e2,
                'trueProp': true,
                'falseProp': false,
                'nullProp': null,
                'aryProp': ['val', -3.14e2, true, false, null],
                'objProp': {'a': 1},
                'emptyAry': [],
                'emptyObhj': {}    
            }".Replace('\'', '"');
            var o = JsonParser.Parse(new StringReader(json));
        }
    }
}
