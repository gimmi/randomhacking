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
            await _sut.StartAsync(IPAddress.Parse("127.0.0.1"), 22, 2222, 10);

            var statusCode = await FtpUploadAsync("ftp://127.0.0.1:22/file.txt", Encoding.ASCII.GetBytes("Hello World!"));
            Assert.That(statusCode, Does.StartWith("226 Transfer complete"));

            await _sut.StopAsync();
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
