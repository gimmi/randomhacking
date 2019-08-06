using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace FTPServer
{
    public class FtpServerTest
    {
        private FtpServer _sut;
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(builder => {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddConsole();
                })
                .AddSingleton<FtpServer>()
                .BuildServiceProvider();

            _sut = _serviceProvider.GetRequiredService<FtpServer>();
        }

        [TearDown]
        public void TearDown()
        {
            _serviceProvider.Dispose();
        }

        [Test]
        public async Task Should_upload_file()
        {
            await _sut.StartAsync(IPAddress.Parse("127.0.0.1"), 22, 2222, 10, async (filePath, stream) => {
                var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                await Console.Out.WriteLineAsync($"{filePath}: {Encoding.ASCII.GetString(ms.ToArray())}");
            });

            var statusCode = await FtpUploadAsync("ftp://127.0.0.1:22/file.txt", Encoding.ASCII.GetBytes("Hello World!"));
            Assert.That(statusCode, Does.StartWith("226 Transfer complete"));

            await _sut.StopAsync();
        }

        [Test]
        public async Task MemoryStream_should_be_reused()
        {
            var ms = new MemoryStream();

            Assert.That(ms.Capacity, Is.Zero);
            Assert.That(ms.GetBuffer(), Is.EqualTo(new byte[0]));

            await new MemoryStream(Enumerable.Repeat<byte>(1, 3).ToArray()).CopyToAsync(ms);

            Assert.That(ms.Capacity, Is.EqualTo(256));
            Assert.That(ms.GetBuffer(), Has.Length.EqualTo(256));
            Assert.That(ms.Length, Is.EqualTo(3));
            Assert.That(ms.GetBuffer()[0], Is.EqualTo(1));
            Assert.That(ms.GetBuffer()[2], Is.EqualTo(1));

            ms.SetLength(0);

            Assert.That(ms.Capacity, Is.EqualTo(256));
            Assert.That(ms.GetBuffer(), Has.Length.EqualTo(256));
            Assert.That(ms.Length, Is.Zero);

            await new MemoryStream(Enumerable.Repeat<byte>(2, 256).ToArray()).CopyToAsync(ms);

            Assert.That(ms.Capacity, Is.EqualTo(256));
            Assert.That(ms.GetBuffer(), Has.Length.EqualTo(256));
            Assert.That(ms.Length, Is.EqualTo(256));
            Assert.That(ms.GetBuffer()[0], Is.EqualTo(2));
            Assert.That(ms.GetBuffer()[255], Is.EqualTo(2));

            ms.SetLength(0);

            await new MemoryStream(Enumerable.Repeat<byte>(3, 257).ToArray()).CopyToAsync(ms);

            Assert.That(ms.Capacity, Is.EqualTo(512));
            Assert.That(ms.GetBuffer(), Has.Length.EqualTo(512));
            Assert.That(ms.Length, Is.EqualTo(257));
            Assert.That(ms.GetBuffer()[0], Is.EqualTo(3));
            Assert.That(ms.GetBuffer()[256], Is.EqualTo(3));
        }

        private static async Task<string> FtpUploadAsync(string requestUri, byte[] fileContents)
        {
            var ftpWebRequest = (FtpWebRequest) WebRequest.Create(requestUri);
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpWebRequest.Credentials = new NetworkCredential("usr", "pwd");
            ftpWebRequest.ContentLength = fileContents.Length;
            using (var requestStream = ftpWebRequest.GetRequestStream())
            {
                await requestStream.WriteAsync(fileContents, 0, fileContents.Length);
            }

            using (var ftpWebResponse = (FtpWebResponse) await ftpWebRequest.GetResponseAsync())
            {
                return ftpWebResponse.StatusDescription;
            }
        }
    }
}
