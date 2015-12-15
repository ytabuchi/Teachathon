using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using System.Diagnostics;
using XF_Stopwatch.Models;

namespace XF_Stopwatch.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        
        Stopwatch sw = new Stopwatch();
        Stopwatch lw = new Stopwatch();
        int lapNumber = 1;

        public MainViewModel()
        {
            this.VmLapTimes = new ObservableCollection<LapTimes>();

            this.StartCommand = new Command(() =>
            {
                VmLapTimes.Clear();
                sw.Reset();
                sw.Start();
                this.IsInLoop = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                {
                    StopwatchMillseconds = sw.ElapsedMilliseconds;

                    return this.IsInLoop;
                });
            });

            this.StopCommand = new Command(() =>
            {
                this.IsInLoop = false;

                sw.Stop();
                lw.Stop();

                if (lapNumber == 1)
                    VmLapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = sw.ElapsedMilliseconds });
                else
                    VmLapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = lw.ElapsedMilliseconds });


            });

            this.LapCommand = new Command(() =>
            {
                if (sw.ElapsedMilliseconds > 0)
                {
                    if (lapNumber == 1)
                    {
                        VmLapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = sw.ElapsedMilliseconds });
                        lw.Start();
                        lapNumber++;
                    }
                    else
                    {
                        VmLapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = lw.ElapsedMilliseconds });
                        lw.Restart();
                        lapNumber++;
                    }
                }
            });

            this.StartStopCommand = new Command(p =>
            {
                if (p.ToString().ToLower() == "start")
                    StartCommand.Execute(true);
                else
                    StopCommand.Execute(true);
            });
        }


        public ICommand StartStopCommand { protected set; get; }
        public ICommand StartCommand { protected set; get; }
        public ICommand StopCommand { protected set; get; }
        public ICommand LapCommand { protected set; get; }

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

        private string _buttonText = "Start"; // これ良いのかな？
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged("ButtonText");
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
                    this.ButtonText = changeText(_isInLoop);
                }
            }
        }

        string changeText(bool b)
        {
            return b ? "Stop" : "Start";
        }

        private ObservableCollection<LapTimes> _vmLapTimes;
        public ObservableCollection<LapTimes> VmLapTimes
        {
            get { return _vmLapTimes; }
            set
            {
                if (_vmLapTimes != value)
                {
                    _vmLapTimes = value;
                    OnPropertyChanged("VmLapTimes");
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
