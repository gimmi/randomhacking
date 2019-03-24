using System;
using Microsoft.Extensions.DiagnosticAdapter;

namespace SpikeMicrosoftExtensions
{
    public class MyDiagnosticListener
    {
        [DiagnosticName("IncomingCall")]
        public virtual void OnIncomingCall(int val)
        {
            Console.WriteLine("IncomingCall: " + val);
        }
    }
}