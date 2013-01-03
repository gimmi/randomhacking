using System;
using System.ServiceModel;

namespace WcfServiceSdk
{
    public class ServiceFactory : IDisposable
    {
        private readonly Lazy<ChannelFactory<IService1>> _channelFactory;

        public ServiceFactory(string uriString)
        {
            _channelFactory = new Lazy<ChannelFactory<IService1>>(() => new ChannelFactory<IService1>(new BasicHttpBinding(), uriString));
        }

        public void Dispose()
        {
            if (_channelFactory.IsValueCreated)
            {
                ((IDisposable) _channelFactory.Value).Dispose();
            }
        }

        public IService1 Create()
        {
            return _channelFactory.Value.CreateChannel();
        }
    }
}
