﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF_Stopwatch
{
    public class App : Application
    {
        public App()
        {
            var nav = new NavigationPage(new Views.MainPageXaml());
            nav.BarBackgroundColor = Color.FromHex("75C465");
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
