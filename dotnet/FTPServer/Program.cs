using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FTPServer
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddConsole();
                })
                .AddSingleton<FtpServer>()
                .BuildServiceProvider();

            var ct = BindCtrlC();

            var theServer = serviceProvider.GetRequiredService<FtpServer>();
            await theServer.StartAsync(IPAddress.Parse("127.0.0.1"), 22, 2222, 10);

            var filePath = "myfile.txt";
            var fileContents = Encoding.ASCII.GetBytes("Hello world");

            await FtpUploadAsync(filePath, fileContents);

            try
            {
                await Task.Delay(Timeout.Infinite, ct);
            }
            catch (OperationCanceledException)
            {
            }

            await Console.Out.WriteLineAsync("Calling disconnect");
            await theServer.StopAsync();
        }

        private static async Task FtpUploadAsync(string filePath, byte[] fileContents)
        {
            var ftpWebRequest = (FtpWebRequest) WebRequest.Create("ftp://127.0.0.1:22/" + filePath);
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpWebRequest.Credentials = new NetworkCredential("usr", "pwd");
            ftpWebRequest.ContentLength = fileContents.Length;
            using (var requestStream = ftpWebRequest.GetRequestStream())
            {
                await requestStream.WriteAsync(fileContents, 0, fileContents.Length);
            }

            using (var ftpWebResponse = (FtpWebResponse) await ftpWebRequest.GetResponseAsync())
            {
                Console.WriteLine($"Upload File Complete, status {ftpWebResponse.StatusDescription}");
            }
        }

        public static CancellationToken BindCtrlC()
        {
            var stopCts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) => {
                e.Cancel = true;
                stopCts.Cancel();
            };
            return stopCts.Token;
        }
    }
}
