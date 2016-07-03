using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace XF_Stopwatch
{
    public class LapTimeViewModel : INotifyPropertyChanged
    {
        private Models.LapTime _lapTime;

        private bool _isRounded;
        public bool IsRounded
        {
            get { return _isRounded; }
            set
            {
                if (_isRounded != value)
                {
                    _isRounded = value;
                    OnPropertyChanged("LapSpan");
                }
            }

        }

        public int Lap
        {
            get { return _lapTime.Lap; }
        }

        public string LapSpan
        {
            get
            {
                if (IsRounded)
                    return _lapTime.LapSpan.ToString(@"mm\:ss");
                else
                    return _lapTime.LapSpan.ToString(@"mm\:ss\.ffff");
            }
        }

        public LapTimeViewModel(Models.LapTime lapTime)
        {
            _lapTime = lapTime;

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

