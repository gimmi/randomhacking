using System;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using NUnit.Framework;
using SharpTestsEx;
using WcfServiceApplication;

namespace WcfServiceLib.Tests
{
    [TestFixture]
    public class Service1FunctionalTest
    {
        [SetUp]
        public void SetUp()
        {
            _uriString = "http://localhost:1234/Service1.svc";
            AppConfigurator.BuildContainer();
            _host = new DefaultServiceHostFactory().CreateServiceHost("WcfServiceApplication.Service1", new[] {new Uri(_uriString)});
            _host.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _host.Close();
        }

        private ServiceHostBase _host;
        private string _uriString;

        [Test]
        public void Tt()
        {
            using (var factory = new ChannelFactory<IService1>(new BasicHttpBinding(), _uriString))
            {
                IService1 channel = factory.CreateChannel();
                channel.Process("xxx").Should().Be.EqualTo("XXX");
            }
        }
    }
}
