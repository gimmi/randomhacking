using System.ServiceModel;

namespace WcfServiceSdk
{
    public class ClientFactory
    {
        private readonly string _uriString;

        public ClientFactory(string uriString)
        {
            _uriString = uriString;
        }

        public IService1 Create()
        {
            return ChannelFactory<IService1>.CreateChannel(new BasicHttpBinding(), new EndpointAddress(_uriString));
        }
    }
}
