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
					subs.Handler<YourMessage>(OnHandleYourMessage);
				});
				sbc.BeforeConsumingMessage(OnBeforeConsumingMessage);
				sbc.AfterConsumingMessage(OnAfterConsumingMessage);
			});
			Bus.Instance.Publish(new YourMessage { Text = "Hi" });
			Console.WriteLine("{0}: waiting... press a key to exit.", Thread.CurrentThread.ManagedThreadId);
			Console.ReadKey();
		}

		private static void OnHandleYourMessage(YourMessage msg)
		{
			Console.WriteLine("{0}: Received '{1}'", Thread.CurrentThread.ManagedThreadId, msg.Text);
		}

		private static void OnAfterConsumingMessage()
		{
			Console.WriteLine("{0}: AfterConsumingMessage", Thread.CurrentThread.ManagedThreadId);
		}

		private static void OnBeforeConsumingMessage()
		{
			Console.WriteLine("{0}: BeforeConsumingMessage", Thread.CurrentThread.ManagedThreadId);
		}
	}
}