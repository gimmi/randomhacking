using System;
using Nancy;
using Nancy.ModelBinding;

namespace SpikeNancyFx
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = GetHome;
            Post["/data"] = GetData;

            
        }

        private object GetData(object arg)
        {
            return this.Bind<Dto>();
        }

        private dynamic GetHome(dynamic p)
        {
            return $"Hello {p.name}!";
        }
    }

    public class Dto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}