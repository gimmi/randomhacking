using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SpikeMicrosoftExtensions;

namespace AspNetCoreSelfHost
{
    public class ValuesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFooService _fooService;

        public ValuesController(IHostingEnvironment hostingEnvironment, IFooService fooService)
        {
            _hostingEnvironment = hostingEnvironment;
            _fooService = fooService;
        }

        [HttpGet("api/values")]
        public object Get()
        {
            _fooService.LogSomething();
            return _hostingEnvironment;
        }
    }
}
