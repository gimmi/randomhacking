using System;
using MassTransit;

namespace SpikeMassTransit
{
	internal class Program
	{
		private static void Main()
		{
			Console.WriteLine("starting...");

			Bus.Initialize(sbc => {
				Console.WriteLine("configuring...");
				sbc.ReceiveFrom("loopback://localhost/mt_test");
				sbc.Subscribe(subs => {
					subs.Handler<YourMessage>(msg => Console.WriteLine(msg.Text));
				});
				sbc.BeforeConsumingMessage(() => {
					Console.WriteLine("before");
				});
				sbc.AfterConsumingMessage(() => {
					Console.WriteLine("after");
				});
			});
			Console.WriteLine("publishing...");
			Bus.Instance.Publish(new YourMessage { Text = "Hi" });
			Console.WriteLine("waiting... press a key to exit.");
			Console.ReadKey();
		}
	}
}