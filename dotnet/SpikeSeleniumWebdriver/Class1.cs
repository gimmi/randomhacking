using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SpikeSeleniumWebdriver
{
	[TestFixture]
	public class Class1
	{
		private IWebDriver driver;

		[SetUp]
		public void SetUp()
		{
			var profile = new FirefoxProfile("FirefoxProfile", true);
			driver = new FirefoxDriver(profile);
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
		}

		[Test]
		public void Tt()
		{
			driver.Url = "http://www.google.com";
			driver.WaitScript("return !!window.Aplication;");

			Assert.That(driver.ExecuteScript<string>("return document.title;"), Is.EqualTo("Google"));
		}
	}
}