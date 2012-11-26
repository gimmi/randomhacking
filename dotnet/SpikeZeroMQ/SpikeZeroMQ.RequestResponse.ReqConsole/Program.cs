using System;
using System.Globalization;
using System.Linq;
using System.Text;
using ZeroMQ;

namespace SpikeZeroMQ.RequestResponse.ReqConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(ZmqVersion.Current.ToString());
            using (ZmqContext context = ZmqContext.Create())
            {
                using (ZmqSocket requester = context.CreateSocket(SocketType.REQ))
                {
                    requester.Connect("tcp://localhost:5555");

	                foreach (var requestNumber in Enumerable.Range(1, 10))
	                {
						Console.WriteLine("Sending request {0}...", requestNumber);
						requester.Send(requestNumber.ToString(CultureInfo.InvariantCulture), Encoding.UTF8);

						string reply = requester.Receive(Encoding.UTF8);
						Console.WriteLine("Received reply {0}: {1}", requestNumber, reply);
	                }
                }
            }

	        Console.ReadKey();

        }
    }
}