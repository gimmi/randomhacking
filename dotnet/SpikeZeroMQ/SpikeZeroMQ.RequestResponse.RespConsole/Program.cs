using System;
using System.Text;
using ZeroMQ;

namespace SpikeZeroMQ.RequestResponse.RespConsole
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(ZmqVersion.Current.ToString());
			using (ZmqContext context = ZmqContext.Create())
			{
				using (ZmqSocket requester = context.CreateSocket(SocketType.REP))
				{
					requester.Connect("tcp://localhost:5555");

					const string requestMessage = "Hello";
					const int requestsToSend = 10;

					for (int requestNumber = 0; requestNumber < requestsToSend; requestNumber++)
					{
						Console.WriteLine("Sending request {0}...", requestNumber);
						requester.Send(requestMessage, Encoding.UTF8);

						string reply = requester.Receive(Encoding.UTF8);
						Console.WriteLine("Received reply {0}: {1}", requestNumber, reply);
					}
				}
			}
		}
	}
}