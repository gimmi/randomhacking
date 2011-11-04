using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NUnit.Framework;
using SharpTestsEx;

namespace SpikeXmlSerialization
{
	public class RssChannel
	{
		[XmlElement("item")]
		public List<RssItem> Items;

		[XmlElement("title")]
		public string Title;
	}

	public class RssItem
	{
		[XmlElement("guid")]
		public string Guid;

		[XmlElement("title")]
		public string Title;
	}

	[XmlRoot("rss")]
	public class Rss
	{
		[XmlElement("channel")]
		public RssChannel Channel;

		[XmlAttribute("version")]
		public string Version;
	}

	[TestFixture]
	public class RssDeserializationtTest
	{
		[Test]
		public void Tt()
		{
			Rss rss;
			using(var streamReader = new StreamReader("rss2sample.xml"))
			{
				rss = (Rss)new XmlSerializer(typeof(Rss)).Deserialize(streamReader);
			}

			rss.Version.Should().Be.EqualTo("2.0");
			rss.Channel.Title.Should().Be.EqualTo("Liftoff News");
			rss.Channel.Items.Should().Have.Count.EqualTo(4);
			rss.Channel.Items.First().Title.Should().Be.EqualTo("Star City");
		}
	}
}