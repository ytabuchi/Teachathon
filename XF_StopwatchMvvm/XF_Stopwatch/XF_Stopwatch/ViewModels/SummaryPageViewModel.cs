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
    public class SummaryPageViewModel : INotifyPropertyChanged
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

        

        public SummaryPageViewModel()
        {
            this.LapTimes = SingletonStopwatchModel.Instance.LapTimes;
            
            //SingletonStopwatchModel.Instance.PropertyChanged += Instance_PropertyChanged;

            this.AscendingSortCommand = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("【AscendingSortCommand】");
                var sortedList = this.LapTimes.OrderBy(x => x.LapSpan.TotalMilliseconds);
                this.LapTimes = new ObservableCollection<LapTime>(sortedList);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LapTimes)));
            });

            this.DescendingSortCommand = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("【DescendingSortCommand】");
                var sortedList = this.LapTimes.OrderByDescending(x => x.LapSpan.TotalMilliseconds);
                this.LapTimes = new ObservableCollection<LapTime>(sortedList);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LapTimes)));
            });
        }

        public ICommand AscendingSortCommand { get; private set; }
        public ICommand DescendingSortCommand { get; private set; }

        //private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //System.Diagnostics.Debug.WriteLine(e.PropertyName);
        //    switch (e.PropertyName)
        //    {
        //        default:
        //            break;
        //    }
        //}

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
