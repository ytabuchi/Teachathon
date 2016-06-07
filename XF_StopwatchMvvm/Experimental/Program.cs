using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experimental
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TimeSpan> list = new List<TimeSpan>()
            {
                new TimeSpan(0, 1, 1000),
                new TimeSpan(0, 0, 5000),
                new TimeSpan(0, 0, 8000)
            };

            foreach (var x in list)
            {
                Console.WriteLine("Ticks: {0}", x.Ticks);
                Console.WriteLine("TotalMilliseconds: {0}", (int)x.TotalMilliseconds);
            }
        }
    }
}
