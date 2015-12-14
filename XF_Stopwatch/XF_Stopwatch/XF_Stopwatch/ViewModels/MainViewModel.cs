using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace XF_Stopwatch.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            
               
        }


        private long _stopwatchMilliseconds;

        public long StopwatchMillseconds
        {
            get { return _stopwatchMilliseconds; }
            set
            {
                if (_stopwatchMilliseconds != value)
                {
                    _stopwatchMilliseconds = value;
                    OnPropertyChanged("StopwatchMillseconds");
                }
            }
        }



        private bool _isInLoop;

        public bool IsInLoop
        {
            get { return _isInLoop; }
            set
            {
                if (_isInLoop != value)
                {
                    _isInLoop = value;
                    OnPropertyChanged("IsInLoop");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
