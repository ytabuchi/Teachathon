using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XF_Stopwatch.Models;
using XF_Stopwatch.ViewModels;

namespace XF_Stopwatch.Views
{
    public partial class MainPageXaml : ContentPage
    {
        public MainPageXaml()
        {
            InitializeComponent();

            // 参考：https://developer.xamarin.com/guides/cross-platform/xamarin-forms/messaging-center/
            // 第2引数が同じSendとSubscribeでやり取りをするようです
            MessagingCenter.Subscribe<MainViewModel, ObservableCollection<LapTimes>>(this, "TotalTime", (sender, arg) =>
            {
                var ms = arg.Sum(l => l.LapTime);
                var ss = ms / 1000;
                ms = ms % 1000;
                var mm = ss / 60;
                ss = ss % 60;

                var max = arg.Max(i => i.LapTime);
                var min = arg.Min(j => j.LapTime);

                var alertTitle = string.Format("Total Time: {0:00}'{1:00}\"{2:000}", mm, ss, ms);

                DisplayAlert(alertTitle, string.Format("Max laptime: {0}ms\nMin laptime: {1}ms", max, min), "OK");
            });
        }
    }
}
