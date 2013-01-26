using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using WcfServiceSdk;

namespace WcfServiceApplication
{
    public static class AppConfigurator
    {
        public static IWindsorContainer BuildContainer()
        {
            IWindsorContainer container = new WindsorContainer();

            container.AddFacility<WcfFacility>();

            container.Register(
               Component.For<IService1>().ImplementedBy<Service1>().LifestylePerWcfOperation(),
               Component.For<Dependency1>().LifestylePerWcfOperation()
               );

            return container;
        }
    }
}
