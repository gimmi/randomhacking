using System;
using System.Web;
using Castle.Windsor;

namespace WcfServiceApplication
{
    public class Global : HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = AppConfigurator.BuildContainer();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (_container != null)
            {
                _container.Dispose();
            }
        }
    }
}
