using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SpikeNancyFx
{
    public sealed class AppJsonSerializer : JsonSerializer
    {
        public AppJsonSerializer()
        {
            Formatting = Formatting.Indented;
            ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}