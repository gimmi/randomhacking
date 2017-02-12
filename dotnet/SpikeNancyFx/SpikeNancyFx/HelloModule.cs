using System;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json.Linq;

namespace SpikeNancyFx
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = GetHome;
            Post["/echo"] = arg => {
                dynamic o = this.Bind();
                Console.WriteLine(o.id);
                return o;
            };
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
        public JObject Props { get; set; }
    }
}