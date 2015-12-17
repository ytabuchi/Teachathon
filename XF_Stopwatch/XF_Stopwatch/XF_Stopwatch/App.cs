using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XF_Stopwatch.Views;
using XF_Stopwatch.Models;

namespace XF_Stopwatch
{
    public class App : Application
    {
        public static ObservableCollection<LapTimes> lapTimes { get; set; }
        public static bool isShowed { get; set; }

        // こんなのあり？
        static long ss;
        static long mm;
        public static string ChangeFormat(long ms)
        {
            ss = ms / 1000;
            ms = ms % 1000;
            mm = ss / 60;
            ss = ss % 60;

            if (isShowed)
                return string.Format("{0:00}'{1:00}\"{2:000}", mm, ss, ms);
            else
                return string.Format("{0:00}'{1:00}\"", mm, ss);

        }

        public App()
        {
            lapTimes = new ObservableCollection<LapTimes>();
            isShowed = true;

            var nav = new NavigationPage(new StartPage());
            nav.BarBackgroundColor = Color.FromHex("3498DB");
            nav.BarTextColor = Color.White;
            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
