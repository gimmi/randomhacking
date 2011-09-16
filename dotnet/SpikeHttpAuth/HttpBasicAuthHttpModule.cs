using System;
using System.Configuration;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace SpikeHttpAuth
{
	public class HttpBasicAuthHttpModule : IHttpModule
	{
		public void Dispose() {}

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += OnAuthenticateRequest;
		}

		public void OnAuthenticateRequest(object source, EventArgs eventArgs)
		{
			var context = (HttpApplication)source;

			string username = null;
			string password = null;

			string authorization = context.Request.Headers["Authorization"] ?? "";
			if(authorization.StartsWith("Basic "))
			{
				byte[] bytes = Convert.FromBase64String(authorization.Substring(6));
				string[] usernamePassword = new ASCIIEncoding().GetString(bytes).Split(':');
				username = usernamePassword[0];
				password = usernamePassword[1];
			}

			if(!AuthenticateAgent(context, username, password))
			{
				context.Response.Status = "401 Unauthorized";
				string realm = ConfigurationSettings.AppSettings[GetType().FullName + ".Realm"];
				context.Response.AppendHeader("WWW-Authenticate", String.Format("Basic Realm=\"{0}\"", realm));
				context.Response.ContentType = "text/plain";
				context.Response.Write(context.Response.StatusDescription);
				context.CompleteRequest();
			}
		}

		protected virtual bool AuthenticateAgent(HttpApplication app, string username, string password)
		{
			string type = GetType().FullName;

			if (string.Equals(username, "gimmi") && string.Equals(password, "ciao"))
			{
				app.Context.User = new GenericPrincipal(new GenericIdentity("Gian Marco Gherardi", type), new[] { "Users", "Administrators" });
			}
			else
			{
				app.Context.User = new GenericPrincipal(new GenericIdentity("", type), new string[0]);
			}
			return true;
		}
	}
}