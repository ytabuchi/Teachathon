using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_Stopwatch.Views
{
    public class StartPage : ContentPage
    {
        public StartPage()
        {
            Title = "Mvvm Teachathon";
            Content = new StackLayout
            {
                Children = {
                    new Button {
                        Text = "Event base",
                        Command = new Command(()=>
                        {
                            Navigation.PushAsync(new MainPageCS());
                        }),
                    },
                    new Button
                    {
                        Text = "Mvvm",
                        Command = new Command(()=>
                        {
                            Navigation.PushAsync(new MainPageXaml());
                        }),
                    }
                }
            };
        }
    }
}
