using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF_Stopwatch.Models
{
    public class LapTime
    {
        public DateTime Time { get; set; }
        public TimeSpan Span { get; set; }

        public LapTime(DateTime time, TimeSpan span)
        {
            this.Time = time;
            this.Span = span;
        }
    }
}
