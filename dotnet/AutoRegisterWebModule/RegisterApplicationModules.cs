using System.Web;
using AutoRegisterWebModule;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: PreApplicationStartMethod(typeof(RegisterApplicationModules), "Run")]

namespace AutoRegisterWebModule
{
	public static class RegisterApplicationModules
	{
		public static void Run()
		{
			DynamicModuleUtility.RegisterModule(typeof(CustomModule));
		}
	}
}