using System;
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
        // Properties
        #region Properties

        private ObservableCollection<LapTimeViewModel> _lapTimes;
        public ObservableCollection<LapTimeViewModel> LapTimes
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
                    foreach (var item in LapTimes)
                    {
                        item.IsRounded = value;
                    }

                    _isRounded = value;
                    OnPropertyChanged();
                    OnPropertyChanged("SpanTime");
                }
            }
        }

        private string _spanTime = @"00:00.0000";
        public string SpanTime
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

        private bool _isInLoop;
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

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public StopwatchPageViewModel()
        {
            // Initialize.
            SingletonStopwatchModel.Instance.LapTimes.Clear();
            SingletonStopwatchModel.Instance.PropertyChanged += Instance_PropertyChanged;

            this.StartCommand = new Command(async () =>
            {
                System.Diagnostics.Debug.WriteLine("【StartCommand】");

                // Invoke StartTimer Method.
                await SingletonStopwatchModel.Instance.StartTimer();
            });

            this.StopCommand = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("【StopCommand】");

                // Invoke StopTimer Method.
                SingletonStopwatchModel.Instance.StopTimer();

                // Invoke GetTotalTime Method and Send it to MessaginCenter with "ShowDialog" Message.
                // MessagingCenter.Send<TSender, TArgs> (TSender sender, string message, TArgs args)
                var totalTime = SingletonStopwatchModel.Instance.GetTotalTime();
                MessagingCenter.Send<StopwatchPageViewModel, string>(this, "ShowDialog", totalTime);
            });

            this.LapCommand = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("【LapCommand】");

                // Invoke LapTimer Method.
                SingletonStopwatchModel.Instance.LapTimer();

                // Send PropertyChanged event of "SpanTime".
                OnPropertyChanged("SpanTime");
            });
        }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand LapCommand { get; private set; }


        /// <summary>
        /// Do something depending on the PropertyChanged events you catched.
        /// </summary>
        /// <returns>The property changed.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"【PropertyName】: {e.PropertyName}");

            switch (e.PropertyName)
            {
                case nameof(SingletonStopwatchModel.SpanTime):
                    if (IsRounded)
                        this.SpanTime = SingletonStopwatchModel.Instance.SpanTime.ToString(@"mm\:ss");
                    else
                        this.SpanTime = SingletonStopwatchModel.Instance.SpanTime.ToString(@"mm\:ss\.ffff");
                    break;
                case nameof(SingletonStopwatchModel.LapTimes):
                    this.LapTimes =                        new ObservableCollection<LapTimeViewModel>(
                            SingletonStopwatchModel.Instance.LapTimes.Select(x => new LapTimeViewModel(x)));
                    break;
                case nameof(SingletonStopwatchModel.IsInLoop):
                    this.IsInLoop = SingletonStopwatchModel.Instance.IsInLoop;
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
