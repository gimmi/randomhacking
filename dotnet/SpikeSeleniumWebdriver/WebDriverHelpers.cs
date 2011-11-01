using System;
using System.Threading;
using OpenQA.Selenium;

namespace SpikeSeleniumWebdriver
{
	public static class WebDriverHelpers
	{
		public static void WaitScript(this IWebDriver driver, string script, int timeoutSeconds = 5, params object[] args)
		{
			var endTime = DateTime.Now.AddSeconds(timeoutSeconds);
			while (DateTime.Now < endTime)
			{
				var ret = driver.ExecuteScript<object>(script, args);
				if(ret != null && !false.Equals(ret))
				{
					return;
				}
				Thread.Sleep(500);
			}
			throw new TimeoutException(string.Format("Waiting for script '{0}' timed out after {1} seconds", script, timeoutSeconds));
		}

		public static T ExecuteScript<T>(this IWebDriver driver, string script, params object[] args)
		{
			return (T)((IJavaScriptExecutor)driver).ExecuteScript(script, args);
		}
	}
}