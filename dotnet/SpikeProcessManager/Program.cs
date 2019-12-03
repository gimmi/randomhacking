using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SpikeProcessManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var workingDirectory = Path.GetDirectoryName(args.First());
            var fileName = Path.GetFullPath(args.First());
            var arguments = string.Join(" ", args.Skip(1));

            Console.Out.WriteLine($"{workingDirectory} {fileName} {arguments}");

            var process = new Process {
                StartInfo = {
                    WorkingDirectory = workingDirectory,
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    //RedirectStandardInput = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
            process.ErrorDataReceived += (_, args) => Console.Out.WriteLine(args.Data);
            process.OutputDataReceived += (_, args) => Console.Error.WriteLine(args.Data);

            Console.Out.WriteLine("Starting app");

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            Console.WriteLine("Process started, press enter to send SIGTERM");
            Console.ReadLine();

            Console.Out.WriteLine("Closing app");
            //process.StandardInput.Close(); // https://stackoverflow.com/a/285041
            //process.CloseMainWindow();

            process.WaitForExit();

            Console.WriteLine("Process exited with exit code " + process.ExitCode);
        }
    }
}
