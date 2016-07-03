using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF_Stopwatch.Models
{
    public class SingletonStopwatchModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static SingletonStopwatchModel Instance { get; } = new SingletonStopwatchModel();

        /// <summary>
        /// Private Constructor for show just one instance is here.
        /// </summary>
        private SingletonStopwatchModel()
        {

        }

        private int _count = 0;

        private bool _isInLoop = false;
        public bool IsInLoop
        {
            get { return _isInLoop; }
            set
            {
                if (_isInLoop != value)
                {
                    _isInLoop = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<LapTime> _lapTimes = new ObservableCollection<LapTime>();
        public ObservableCollection<LapTime> LapTimes
        {
            get { return _lapTimes; }
            set
            {
                if (_lapTimes != value)
                {
                    _lapTimes = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _nowTime;
        public DateTime NowTime
        {
            get { return _nowTime; }
            set
            {
                if (_nowTime != value)
                {
                    _nowTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private TimeSpan _spanTime;
        public TimeSpan SpanTime
        {
            get { return _spanTime; }
            set
            {
                if (_spanTime != value)
                {
                    _spanTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRounded;
        public bool IsRounded
        {
            get { return _isRounded; }
            set
            {
                if (_isRounded != value)
                {
                    _isRounded = value;
                    OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Start the timer.
        /// </summary>
        public async Task StartTimer()
        {
            System.Diagnostics.Debug.WriteLine("【StartTimer()】");

            // Initialize
            _count = 0;
            this.LapTimes.Clear();
            _isInLoop = true;
            _nowTime = DateTime.Now;
            _startTime = _nowTime;

            // Start timer and loop
            while (_isInLoop)
            {
                await Task.Delay(100);
                _nowTime = DateTime.Now;
                this.SpanTime = _nowTime - _startTime;

                System.Diagnostics.Debug.WriteLine(
                    $"【InLoop】Start:{this.StartTime}, Now:{this.NowTime}, Span:{this.SpanTime}");
            }
        }

        public void StopTimer()
        {
            System.Diagnostics.Debug.WriteLine("【StopTimer()】");

            _isInLoop = false;
            _count++;
            this.LapTimes.Insert(0, new LapTime(_count, this.StartTime, this.NowTime, this.SpanTime));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LapTimes)));
        }

        public void LapTimer()
        {
            System.Diagnostics.Debug.WriteLine("【LapTimer()】");

            _count++;
            this.LapTimes.Insert(0, new LapTime(_count, this.StartTime, this.NowTime, this.SpanTime));
            this.StartTime = DateTime.Now;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LapTimes)));
        }

        public string GetTotalTime()
        {
            System.Diagnostics.Debug.WriteLine("【GetTotalTime()】");

            var totalMilliSeconds = Instance.LapTimes.Sum(x => x.LapSpan.TotalMilliseconds);

            var totaltTimeSpan = new TimeSpan(0, 0, 0, 0, (int)totalMilliSeconds);
            return totaltTimeSpan.ToString();
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
