using System.Threading.Tasks;
using CommandLine;

namespace IotHubCli
{
    public static class Program
    {
        public static Task<int> Main(string[] args) => Parser.Default.ParseArguments<PingCommand, RestartModuleCommand, GetModuleLogsCommand>(args).MapResult(
            (PingCommand c) => c.RunAsync(),
            (RestartModuleCommand c) => c.RunAsync(),
            (GetModuleLogsCommand c) => c.RunAsync(),
            _ => Task.FromResult(1)
        );
    }
}
