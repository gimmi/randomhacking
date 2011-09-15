using System.Web;

namespace SpikeHttpAuth
{
	public class Handler : IHttpHandler
	{
		public bool IsReusable
		{
			get { return false; }
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			context.Response.Write(context.User.Identity.Name);
		}
	}
}