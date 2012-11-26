using System;
using System.Text;
using ZeroMQ;

namespace SpikeZeroMQ.RequestResponse.RepConsole
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(ZmqVersion.Current.ToString());
			using (ZmqContext context = ZmqContext.Create())
			{
				using (ZmqSocket repSocket = context.CreateSocket(SocketType.REP))
				{
					repSocket.Bind("tcp://*:5555");

					while (true)
					{
						var msg = repSocket.Receive(Encoding.UTF8);
						Console.WriteLine("Received: " + msg);
						repSocket.Send("REP: " + msg, Encoding.UTF8);
					}
				}
			}
		}
	}
}