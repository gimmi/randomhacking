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
                'emptyObj': {}    
            }".Replace('\'', '"');
            dynamic o = JsonParser.Parse(new StringReader(json));

			Assert.That(o, Has.Count.EqualTo(9));
			Assert.That(o["strProp"], Is.EqualTo("val"));
			Assert.That(o["numProp"], Is.EqualTo(-314));
			Assert.That(o["trueProp"], Is.True);
			Assert.That(o["falseProp"], Is.False);
			Assert.That(o["nullProp"], Is.Null);
			Assert.That(o["aryProp"], Has.Count.EqualTo(5));
			Assert.That(o["aryProp"][0], Is.EqualTo("val"));
			Assert.That(o["aryProp"][1], Is.EqualTo(-314));
			Assert.That(o["aryProp"][2], Is.True);
			Assert.That(o["aryProp"][3], Is.False);
			Assert.That(o["aryProp"][4], Is.Null);
			Assert.That(o["objProp"], Has.Count.EqualTo(1));
			Assert.That(o["objProp"]["a"], Is.EqualTo(1));
			Assert.That(o["emptyAry"], Has.Count.EqualTo(0));
			Assert.That(o["emptyObj"], Has.Count.EqualTo(0));
        }
    }
}
