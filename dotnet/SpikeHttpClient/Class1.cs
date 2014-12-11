using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NUnit.Framework;

namespace SpikeHttpClient
{
	[TestFixture]
    public class Class1
    {
		[Test]
		public void Tt()
		{
			var values = new[] { "?v=1", "?v=2" };
			// http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
			using (var httpClient = new HttpClient(new HttpClientHandler {UseDefaultCredentials = true}, true))
			{
				httpClient.BaseAddress = new Uri("http://httpbin.org/get");
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var tasks = values.Select(httpClient.GetAsync).ToArray();


				var qs = HttpUtility.ParseQueryString(string.Empty);
				qs.Add("v", "1");

				foreach (var task in tasks)
				{
					var result = task.Result.EnsureSuccessStatusCode().Content.ReadAsAsync<object>().Result;
				}
			}
		}
    }
}
