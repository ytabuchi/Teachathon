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
using XF_Stopwatch.Views;

namespace XF_Stopwatch.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        Stopwatch sw = new Stopwatch();
        Stopwatch lw = new Stopwatch();
        long ms;
        int lapNumber = 1;

        /// <summary>
        /// ViewModelコンストラクタ
        /// 参考：https://github.com/ytabuchi/decode/blob/master/decode01/decode01/decode01/Page2Data.cs
        /// </summary>
        public MainViewModel()
        {
            this.VmLapTimes = new ObservableCollection<LapTimes>();

            // Commandの使用方法は以下を参考に
            // 参考：https://developer.xamarin.com/guides/cross-platform/xamarin-forms/user-interface/xaml-basics/data_bindings_to_mvvm/#Commanding_with_ViewModels
            this.StartCommand = new Command(() =>
            {
                VmLapTimes.Clear(); // UWPで"System.ArgumentOutOfRangeException"が出ます？
                this.lapNumber = 1;
                sw.Restart();
                this.IsInLoop = true;

                // 10ミリ秒ごとにチェックされ、trueな限り継続
                Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                {
                    StopwatchMillseconds = sw.ElapsedMilliseconds;

                    return this.IsInLoop;
                });
            });

            this.StopCommand = new Command(() =>
            {
                this.IsInLoop = false; // Device.StartTimerの次のチェック時に停止
                sw.Stop();
                lw.Stop();

                if (lapNumber == 1)
                    ms = sw.ElapsedMilliseconds;
                else
                    ms = lw.ElapsedMilliseconds;

                VmLapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = ms });

                // 参考：https://developer.xamarin.com/guides/cross-platform/xamarin-forms/messaging-center/
                // 第2引数が同じSendとSubscribeでやり取りをするようです
                // 何故簡略化できるのか不明
                MessagingCenter.Send<MainViewModel, ObservableCollection<LapTimes>>(this, "TotalTime", VmLapTimes);
            });

            this.LapCommand = new Command(() =>
            {
                if (sw.ElapsedMilliseconds > 0)
                {
                    if (lapNumber == 1)
                        ms = sw.ElapsedMilliseconds;
                    else
                        ms = lw.ElapsedMilliseconds;

                    VmLapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = ms });
                    lw.Restart();
                    lapNumber++;
                }
            });

            // 参考：https://developer.xamarin.com/api/type/Xamarin.Forms.Command/
            this.StartStopCommand = new Command(p =>
            {
                if (p.ToString().ToLower() == "start")
                    StartCommand.Execute(true);
                else
                    StopCommand.Execute(true);
            });
        }


        public ICommand StartCommand { protected set; get; }
        public ICommand StopCommand { protected set; get; }
        public ICommand LapCommand { protected set; get; }
        public ICommand StartStopCommand { protected set; get; }

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
                    this.ButtonText = changeText(_isInLoop); // Viewに影響を与えてるけど良いのかな？
                }
            }
        }

        private string changeText(bool b)
        {
            return b ? "Stop" : "Start";
        }


        // TODO：これを参照できるように…
        //private bool _isShowed = true;
        //public bool IsShowed
        //{
        //    get { return _isShowed; }
        //    set
        //    {
        //        if (_isShowed != value)
        //        {
        //            _isShowed = value;
        //            OnPropertyChanged("IsShowed");
        //        }
        //    }
        //}



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
