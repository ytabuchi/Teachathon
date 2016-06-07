using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using XF_Stopwatch.Models;

namespace XF_Stopwatch.ViewModels
{
    public class StopwatchPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<LapTime> _lapTimes;
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

        private TimeSpan _spanTime;
        public TimeSpan SpanTime
        {
            get
            {
                return _spanTime;
            }
            set
            {
                if (_spanTime != value)
                {
                    _spanTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public StopwatchPageViewModel()
        {
            SingletonStopwatchModel.Instance.LapTimes.Clear();
            SingletonStopwatchModel.Instance.PropertyChanged += Instance_PropertyChanged;

            this.StartCommand = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("【StartCommand】");
                SingletonStopwatchModel.Instance.StartTimer();

            });

            this.StopCommand = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("【StopCommand】");
                SingletonStopwatchModel.Instance.StopTimer();
                //this.SpanTime = SingletonStopwatchModel.Instance.LapTimes.Last().LapSpan;

                var totalTime = SingletonStopwatchModel.Instance.GetTotalTime();
                MessagingCenter.Send<StopwatchPageViewModel, string>(this, "ShowDialog", totalTime);
            });

            this.LapCommand = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("【LapCommand】");
                SingletonStopwatchModel.Instance.LapTimer();
            });
        }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand LapCommand { get; private set; }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.PropertyName);
            switch (e.PropertyName)
            {
                case nameof(SingletonStopwatchModel.SpanTime):
                    this.SpanTime = SingletonStopwatchModel.Instance.SpanTime;
                    //if (IsRounded)
                    //    this.SpanTime = SingletonStopwatchModel.Instance.SpanTime.ToString(@"mm\:ss");
                    //else
                    //    this.SpanTime = SingletonStopwatchModel.Instance.SpanTime.ToString(@"mm\:ss\.ffff");
                    break;
                case nameof(SingletonStopwatchModel.LapTimes):
                    this.LapTimes = SingletonStopwatchModel.Instance.LapTimes;
                    break;
                default:
                    break;
            }
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
