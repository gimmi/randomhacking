using System;
using System.Diagnostics;
using System.Web;

namespace SpikeHttpAuth
{
	public class UnitOfWorkHttpModule : IHttpModule
	{
		public void Dispose() {}

		public void Init(HttpApplication context)
		{
			context.BeginRequest += OnBeginRequest;
			context.EndRequest += OnEndRequest;
		}

		private void OnEndRequest(object sender, EventArgs e)
		{
			var context = (HttpApplication)sender;
			context.Context.Items.Remove("session");
			Debug.WriteLine("UoW destroyed");
		}

		private void OnBeginRequest(object sender, EventArgs e)
		{
			var context = (HttpApplication)sender;
			context.Context.Items.Add("session", new object());
			Debug.WriteLine("UoW created");
		}
	}
}