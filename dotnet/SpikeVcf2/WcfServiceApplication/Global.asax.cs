using System;
using System.ServiceModel;
using System.Web;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace WcfServiceApplication
{
    public class Global : HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
//            var baseUri = new Uri(HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped));

            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>();
            _container.Register(
                Component.For<IService1>()
                         .ImplementedBy<Service1>()
//                         .AsWcfService(new DefaultServiceModel().Hosted()
//                                                                .PublishMetadata(x => x.EnableHttpGet())
//                                                                .AddBaseAddresses(new Uri(baseUri, "Service1.svc"))
//                                                                .AddEndpoints(WcfEndpoint.BoundTo(new BasicHttpBinding()).At("Soap11"), WcfEndpoint.BoundTo(new WSHttpBinding()).At("Soap12"))
//                    )
                         .LifestylePerWcfOperation()
                );
            _container.Register(
                Component.For<Dependency1>()
                         .LifestylePerWcfOperation()
                );
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
