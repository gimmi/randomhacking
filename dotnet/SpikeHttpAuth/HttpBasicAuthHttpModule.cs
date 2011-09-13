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

			string authorization = context.Request.Headers["Authorization"] ?? "";
			if(!authorization.StartsWith("Basic ", 0))
			{
				AccessDenied(context);
				return;
			}

			byte[] bytes = Convert.FromBase64String(authorization.Substring(6));
			string userInfo = new ASCIIEncoding().GetString(bytes);

			string[] usernamePassword = userInfo.Split(':');
			string username = usernamePassword[0];
			string password = usernamePassword[1];

			string[] groups;
			if(AuthenticateAgent(context, username, password, out groups))
			{
				context.Context.User = new GenericPrincipal(new GenericIdentity(username, GetType().FullName), groups);
			}
			else
			{
				AccessDenied(context);
				return;
			}
		}

		private void AccessDenied(HttpApplication app)
		{
			app.Response.Status = "401 Unauthorized";
			var realm = ConfigurationSettings.AppSettings[GetType().FullName + ".Realm"];
			app.Response.AppendHeader("WWW-Authenticate", String.Format("Basic Realm=\"{0}\"", realm));
			app.Response.ContentType = "text/plain";
			app.Response.Write(app.Response.StatusDescription);
			app.CompleteRequest();
		}


		protected virtual bool AuthenticateAgent(HttpApplication app, string username, string password, out string[] groups)
		{
			groups = new string[0];
			return true;
		}
	}
}