using System.Diagnostics;
using System.Web;

namespace AutoRegisterWebModule
{
	public class CustomModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.BeginRequest += (source, eventArgs) => BeginRequest();
		}

		public void Dispose() { }

		private void BeginRequest()
		{
			Debug.WriteLine("BeginRequest " + GetType());
		}
	}
}