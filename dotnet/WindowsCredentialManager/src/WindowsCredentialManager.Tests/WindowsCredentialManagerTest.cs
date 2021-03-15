using NUnit.Framework;

namespace WindowsCredentialManager.Tests
{
    public class WindowsCredentialManagerTests
    {
        private WindowsCredentialManager _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new WindowsCredentialManager();
        }

        [Test]
        public void Should_store_and_retrieve_creds()
        {
            _sut.AddOrUpdate("wcm-test", "usr1", "pwd1");

            var found = _sut.TryGet("wcm-test", out var username, out var password);

            Assert.That(found, Is.True);
            Assert.That(username, Is.EqualTo("usr1"));
            Assert.That(password, Is.EqualTo("pwd1"));

            _sut.AddOrUpdate("wcm-test", "usr2", "pwd2");

            found = _sut.TryGet("wcm-test", out username, out password);

            Assert.That(found, Is.True);
            Assert.That(username, Is.EqualTo("usr2"));
            Assert.That(password, Is.EqualTo("pwd2"));

            var removed = _sut.TryRemove("wcm-test");

            Assert.That(removed, Is.True);

            found = _sut.TryGet("wcm-test", out username, out password);

            Assert.That(found, Is.False);

            removed = _sut.TryRemove("wcm-test");

            Assert.That(removed, Is.False);
        }
    }
}