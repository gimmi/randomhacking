using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTPServer
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var ct = BindCtrlC();
            var theServer = new TcpServer();
            await theServer.ConnectAsync(22);

            var filePath = "myfile.txt";
            var fileContents = Encoding.ASCII.GetBytes("Hello world");

            await FtpUploadAsync(filePath, fileContents);

            await Task.Yield();
            try
            {
                await Task.Delay(Timeout.Infinite, ct);
            }
            catch (OperationCanceledException)
            {
            }


            await Console.Out.WriteLineAsync("Calling disconnect");
            await theServer.DisconnectAsync();
        }

        private static async Task FtpUploadAsync(string filePath, byte[] fileContents)
        {
            var ftpWebRequest = (FtpWebRequest) WebRequest.Create("ftp://127.0.0.1:22/" + filePath);
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpWebRequest.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");
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
