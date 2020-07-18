using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConnectionStateMachine
{
    public class ConnectionStateMachineTest
    {
        [Test]
        public async Task Client_and_driver_interaction()
        {
            //var writer = Console.Out;
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);

            var stateMachine = new ConnectionStateMachine();

            await stateMachine.WaitForStateAsync(ConnectionStates.Disconnected, 10);

            await Task.WhenAll(
                Task.Run(async () => {
                    await Task.Delay(2_000);
                    await writer.WriteLineAsync("DRIVER: Starting");

                    await stateMachine.WaitForStateAsync(ConnectionStates.Disconnected, 10);

                    await writer.WriteLineAsync("DRIVER: Disconnected, lets begin connection procedure");

                    await stateMachine.ChangeAsync(ConnectionStates.Connecting);

                    await writer.WriteLineAsync("DRIVER: Connecting");

                    await Task.Delay(1_000);

                    await stateMachine.ChangeAsync(ConnectionStates.Connected);

                    await writer.WriteLineAsync("DRIVER: Connected");

                }),
                Task.Run(async () => {
                    await Task.Delay(1_000);
                    await writer.WriteLineAsync("CLIENT: Starting");

                    await stateMachine.WaitForStateAsync(ConnectionStates.Disconnected, 10);

                    await writer.WriteLineAsync("CLIENT: State is disconnected now, lets wait for connection to be established");

                    await stateMachine.WaitForStateAsync(ConnectionStates.Connected, 10_000);

                    await writer.WriteLineAsync("CLIENT: Connection is established, can use it");
                })
            );

            Assert.That(sb.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), Is.EqualTo(new[] {
                "CLIENT: Starting",
                "CLIENT: State is disconnected now, lets wait for connection to be established",
                "DRIVER: Starting",
                "DRIVER: Disconnected, lets begin connection procedure",
                "DRIVER: Connecting",
                "DRIVER: Connected",
                "CLIENT: Connection is established, can use it",
            }));
        }
    }
}
