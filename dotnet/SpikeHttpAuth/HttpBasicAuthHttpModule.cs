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
			HttpBasicAuth.AuthenticateRequest(application, _realm, AuthenticateAgent);
		}

		protected virtual bool AuthenticateAgent(HttpApplication app, string username, string password)
		{
			string type = GetType().FullName;

			if (string.Equals(username, "gimmi") && string.Equals(password, "ciao"))
			{
				app.Context.User = new GenericPrincipal(new GenericIdentity("Gian Marco Gherardi", type), new[] { "Users", "Administrators" });
				return true;
			}
			return false; // app.Context.User = new GenericPrincipal(new GenericIdentity("", type), new string[0]);
		}
	}
}