using Nancy;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace SpikeNancyFx
{
    public class AppBootstrapper : DefaultNancyBootstrapper
    {
        protected override DiagnosticsConfiguration DiagnosticsConfiguration =>
            new DiagnosticsConfiguration {Password = "secret"};

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<JsonSerializer, AppJsonSerializer>();
        }
    }
}