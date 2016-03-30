using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF_Stopwatch.Models
{
    public class Stopwatch : INotifyPropertyChanged
    {
        StopwatchMode mode = new StopwatchMode();
        Task _task;
        bool _inloop;

        public Stopwatch()
        {
            
        }

        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    //OnPropertyChanged(nameof(this.StartTime));
                }
            }
        }

        private DateTime now;
        public DateTime Now
        {
            get { return now; }
            set
            {
                if (now != value)
                {
                    now = value;
                    OnPropertyChanged(nameof(this.Now));
                }
            }
        }

        public TimeSpan NowTime
        {
            get { return this.Now - this.StartTime; }
        }



        public void Start()
        {
            startTime = DateTime.Now;
            now = startTime;
            mode = StopwatchMode.Start;
            _task = new Task(async () =>
            {
                while (_inloop == true)
                {
                    await Task.Delay(100);
                    now = DateTime.Now;
                }
            });
            _task.Start();
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
