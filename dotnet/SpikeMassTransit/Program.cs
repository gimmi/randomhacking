using System;
using System.Threading;
using MassTransit;

namespace SpikeMassTransit
{
	internal class Program
	{
		private static void Main()
		{
			Bus.Initialize(sbc => {
				sbc.ReceiveFrom("loopback://localhost/mt_test");
				sbc.Subscribe(subs => {
					subs.Handler<YourMessage>(msg => Console.WriteLine("{0}: Received '{1}'", Thread.CurrentThread.ManagedThreadId, msg.Text));
				});
				sbc.BeforeConsumingMessage(() => {
					Console.WriteLine("{0}: BeforeConsumingMessage", Thread.CurrentThread.ManagedThreadId);
				});
				sbc.AfterConsumingMessage(() => {
					Console.WriteLine("{0}: AfterConsumingMessage", Thread.CurrentThread.ManagedThreadId);
				});
			});
			Bus.Instance.Publish(new YourMessage { Text = "Hi" });
			Console.WriteLine("{0}: waiting... press a key to exit.", Thread.CurrentThread.ManagedThreadId);
			Console.ReadKey();
		}
	}
}