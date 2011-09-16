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
			context.Response.Write("Request.IsAuthenticated: " + context.Request.IsAuthenticated + "\n");
			context.Response.Write("User.Identity.Name: " + context.User.Identity.Name + "\n");
			context.Response.Write("User.Identity.AuthenticationType: " + context.User.Identity.AuthenticationType + "\n");
		}
	}
}