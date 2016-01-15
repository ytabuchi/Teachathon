using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF_Stopwatch.Models
{
    class LapTime
    {
        public TimeSpan Span { get; }
        public DateTime Time { get; }

        public LapTime(DateTime time, TimeSpan span)
        {
            this.Time = time;
            this.Span = span;
        }
    }
}
