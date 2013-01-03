using System;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using NUnit.Framework;
using SharpTestsEx;
using WcfServiceApplication;
using WcfServiceSdk;

namespace WcfServiceLib.Tests
{
    [TestFixture]
    public class Service1FunctionalTest
    {
        private ServiceHostBase _host;
        private ClientFactory _clientFactory;

        [SetUp]
        public void SetUp()
        {
            const string uriString = "http://localhost:1234/Service1.svc";
            _clientFactory = new ClientFactory(uriString);
            AppConfigurator.BuildContainer();
            _host = new DefaultServiceHostFactory().CreateServiceHost("WcfServiceApplication.Service1", new[] {new Uri(uriString)});
            _host.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _host.Close();
        }

        [Test]
        public void Should_invoke_service()
        {
            _clientFactory.Create().Process("xxx").Should().Be.EqualTo("XXX");
        }

        [Test]
        public void Should_create_disposable_services()
        {
            _clientFactory.Create().Should().Be.InstanceOf<IDisposable>();
        }
    }
}
