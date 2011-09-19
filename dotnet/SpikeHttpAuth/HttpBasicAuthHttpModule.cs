using System;
using System.Configuration;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace SpikeHttpAuth
{
	public class HttpBasicAuthHttpModule : IHttpModule
	{
		private readonly string _realm;

		public HttpBasicAuthHttpModule() : this(ConfigurationSettings.AppSettings[typeof(HttpBasicAuthHttpModule).FullName + ".Realm"]) { }

		public HttpBasicAuthHttpModule(string realm)
		{
			_realm = realm;
		}

		public void Dispose() {}

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += (source, eventArgs) => AuthenticateRequest((HttpApplication)source);
		}

		public void AuthenticateRequest(HttpApplication application)
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

			if(!AuthenticateAgent(application, username, password))
			{
				application.Response.Status = "401 Unauthorized";
				application.Response.AppendHeader("WWW-Authenticate", String.Format("Basic Realm=\"{0}\"", _realm));
				application.Response.ContentType = "text/plain";
				application.Response.Write(application.Response.StatusDescription);
				application.CompleteRequest();
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