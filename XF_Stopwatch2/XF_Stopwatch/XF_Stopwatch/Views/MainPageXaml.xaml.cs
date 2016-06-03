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
            // ここにこんなに盛り込んでいいのか？
            // App.ChangeFormatはApp.isShowedを参照してしまっている…
            MessagingCenter.Subscribe<MainViewModel, ObservableCollection<LapTimes>>(this, "TotalTime", async (sender, arg) =>
            {
                var ms = arg.Sum(l => l.LapTime);

                var max = App.ChangeFormat(arg.Max(i => i.LapTime));
                var min = App.ChangeFormat(arg.Min(j => j.LapTime));

                var alertTitle = string.Format("Total Time: {0}",App.ChangeFormat(ms));

                var res = await DisplayAlert(alertTitle, string.Format("Max laptime: {0}\nMin laptime: {1}\n\nShow all lap result?", max, min), "Yes", "No");

                if (res)
                    await Navigation.PushAsync(new ResultPage(App.lapTimes)); // TODO：VMのVmLapTimesを参照したい。して良い？出来る？
                else
                {
                    App.lapTimes.Clear(); // TODO：VMのVmLapTimesを参照したい。して良い？出来る？
                }
            });
        }
    }
}
