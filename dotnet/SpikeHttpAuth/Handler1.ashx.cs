using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpikeHttpAuth
{
	/// <summary>
	/// Summary description for Handler1
	/// </summary>
	public class Handler1 : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			HttpResponse response = context.Response;
			response.ContentType = "text/plain";
			response.Write("Hello World");
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}