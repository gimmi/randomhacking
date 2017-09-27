using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpikeCefSharp
{
    public class Backend
    {
        public string Greet(string subject)
        {
            Debug.WriteLine("Greet invoked");
            return $"Hello {subject}!";
        }

        public object RoundtripComplexObj(object input)
        {
            Debug.WriteLine("RoundtripComplexObj invoked");
            return "ciao";
        }
    }
}
