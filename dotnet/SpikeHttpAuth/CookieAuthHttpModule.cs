using System;
using System.IO;
using System.Security.Cryptography;
using System.Web;

namespace SpikeHttpAuth
{
	public class CookieAuthHttpModule : IHttpModule
	{
		public static string CookieName = "auth";
		public static string Key = "RrfW8Z5/L6Eoq9pS+VgZ9qbFo1BHi9Ec5sSDitHz1GI=";
		public static string Iv = "T2jzv2xIFKQY1wBnkgztYQ==";
		public static Func<string, bool> AuthenticateAgent = delegate { return true; };

		public void Dispose() {}

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += (source, eventArgs) => AuthenticateRequest();
		}

		public void AuthenticateRequest()
		{
			HttpApplication application = HttpContext.Current.ApplicationInstance;
			string id = GetId(application.Request);
			if(!AuthenticateAgent(id))
			{
				application.Response.Status = "500 Internal Server Error";
				application.Response.ContentType = "text/plain";
				application.Response.Write(application.Response.StatusDescription);
				application.CompleteRequest();
			}
		}

		//protected virtual bool AuthenticateAgent(HttpApplication app, string id)
		//{
		//    string type = GetType().FullName;
		//    if(string.Equals(id, "gimmi"))
		//    {
		//        app.Context.User = new GenericPrincipal(new GenericIdentity("Gian Marco Gherardi", type), new[] { "Users", "Administrators" });
		//    }
		//    else
		//    {
		//        app.Context.User = new GenericPrincipal(new GenericIdentity("", type), new string[0]);
		//    }
		//    return true;
		//}

		private string GetId(HttpRequest request)
		{
			HttpCookie cookie = request.Cookies.Get(CookieName);
			return cookie == null ? null : Decrypt(cookie.Value);
		}

		public void SetId(string id, bool keep)
		{
			var cookie = new HttpCookie(CookieName);
			if(id != null)
			{
				cookie.Value = Encrypt(id);
				if(keep)
				{
					cookie.Expires = DateTime.Now.AddYears(50);
				}
			}
			else
			{
				cookie.Expires = DateTime.Now.AddDays(-1);
			}
			HttpContext.Current.Response.SetCookie(cookie);
		}

		private string Encrypt(string value)
		{
			var memoryStream = new MemoryStream();
			using(var cryptoStream = new CryptoStream(memoryStream, SymmetricAlgorithm.Create().CreateEncryptor(Convert.FromBase64String(Key), Convert.FromBase64String(Iv)), CryptoStreamMode.Write))
			{
				using(var streamWriter = new StreamWriter(cryptoStream))
				{
					streamWriter.Write(value);
				}
			}
			return Convert.ToBase64String(memoryStream.ToArray());
		}

		private string Decrypt(string value)
		{
			using(var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
			{
				using(var cryptoStream = new CryptoStream(memoryStream, SymmetricAlgorithm.Create().CreateDecryptor(Convert.FromBase64String(Key), Convert.FromBase64String(Iv)), CryptoStreamMode.Read))
				{
					using(var srDecrypt = new StreamReader(cryptoStream))
					{
						return srDecrypt.ReadToEnd();
					}
				}
			}
		}
	}
}