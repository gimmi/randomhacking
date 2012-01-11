using System;
using System.Net;
using System.Text;
using System.Web;

namespace SpikeHttpAuth
{
	public class HttpBasicAuth
	{
		public static void AuthenticateRequest(string realm, Func<string, string, bool> authenticate)
		{
			AuthenticateRequest(HttpContext.Current.ApplicationInstance, realm, (application, username, password) => authenticate.Invoke(username, password));
		}

		public static void AuthenticateRequest(HttpApplication application, string realm, Func<HttpApplication, string, string, bool> authenticate)
		{
			string username = null;
			string password = null;

			string authorization = application.Request.Headers["Authorization"] ?? "";
			if(authorization.StartsWith("Basic "))
			{
				byte[] bytes = Convert.FromBase64String(authorization.Substring(6));
				string[] usernamePassword = new ASCIIEncoding().GetString(bytes).Split(':');
				username = usernamePassword[0];
				password = usernamePassword[1];
			}

			if(!authenticate.Invoke(application, username, password))
			{
				application.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
				application.Response.AppendHeader("WWW-Authenticate", String.Format("Basic Realm=\"{0}\"", realm));
				application.Response.ContentType = "text/plain";
				application.Response.Write(application.Response.StatusDescription);
				application.CompleteRequest();
			}
		}
	}
}