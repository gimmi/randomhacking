using System;
using System.Diagnostics;

namespace SpikeProcessManager
{
    public class Program
    {
        public static void Main()
        {
            var process = new Process {
                StartInfo = {
                    FileName = GraphicsMagicExePath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    //RedirectStandardInput = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
            process.ErrorDataReceived += (_, args) => Console.Out.Write(args.Data);
            process.OutputDataReceived += (_, args) => Console.Error.Write(args.Data);

            Console.Out.WriteLine("Starting app");

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            Console.WriteLine("Process started, press enter to send SIGTERM");
            Console.ReadLine();

            // https://stackoverflow.com/a/285041
            process.StandardInput.Close();

            process.WaitForExit();

            Console.WriteLine("Process exited with exit code " + process.ExitCode);
        }
    }
}
