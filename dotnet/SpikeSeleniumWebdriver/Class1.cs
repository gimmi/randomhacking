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
			driver.Navigate().GoToUrl("http://www.google.com");
			IWebElement query = driver.FindElement(By.Name("q"));
			query.SendKeys("Cheese");
			Assert.That(driver.Title, Is.EqualTo("Google"));
		}
	}
}