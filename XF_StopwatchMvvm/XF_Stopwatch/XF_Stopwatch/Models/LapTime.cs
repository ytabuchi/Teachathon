using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF_Stopwatch.Models
{
    public class LapTime
    {
        public int Lap { get; }
        public DateTime LapStart { get; }
        public DateTime LapEnd { get; }
        public TimeSpan LapSpan { get; }

        public LapTime(int lap, DateTime lapStart, DateTime lapEnd, TimeSpan lapSpan)
        {
            this.Lap = lap;
            this.LapStart = lapStart;
            this.LapEnd = lapEnd;
            this.LapSpan = lapSpan;
        }
    }
}
