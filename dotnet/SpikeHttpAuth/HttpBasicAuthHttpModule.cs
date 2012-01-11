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

		/* This snippet shows authentication using MembershipProvider of ASP.NET.

in web.config:
    <system.web>
        <compilation debug="true" targetFramework="4.0" />
		<machineKey validationKey="xxxxx" decryptionKey="xxxxx" decryption="3DES" validation="SHA1" />
		<membership defaultProvider="SqlMembershipProvider">
			<providers>
				<clear />
				<add name="SqlMembershipProvider" 
					 type="System.Web.Security.SqlMembershipProvider" 
					 connectionStringName="SqlMembershipProvider"
					 enablePasswordRetrieval="true" 
					 enablePasswordReset="true" 
					 requiresQuestionAndAnswer="false" 
					 minRequiredPasswordLength="7" 
					 minRequiredNonalphanumericCharacters="0" 
					 requiresUniqueEmail="false" 
					 passwordFormat="Encrypted" 
					 applicationName="DotNetNuke" 
					 description="Stores and retrieves membership data from the local Microsoft SQL Server database" />
			</providers>
		</membership>
	</system.web>

//			MembershipUser membershipUser = Membership.GetUser("9182");
//			bool unlockUser = membershipUser.UnlockUser();
//			string pwd = membershipUser.GetPassword();
			HttpBasicAuth.AuthenticateRequest("Insert just the user name", delegate(string username, string password) {
				if(!Membership.ValidateUser(username, password))
				{
					return false;
				}
				HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(Membership.GetUser(username).UserName, "HTTP Basic Auth"), null);
				return true;
			});
		 
		 */
	}
}