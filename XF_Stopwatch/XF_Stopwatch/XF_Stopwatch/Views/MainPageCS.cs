using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using XF_Stopwatch.Models;

namespace XF_Stopwatch.Views
{
    public class MainPageCS : ContentPage
    {
        Label timeLabel;
        ListView lapList;
        Button startButton;
        Button lapButton;
        
        Stopwatch sw = new Stopwatch();
        Stopwatch lw = new Stopwatch();
        long ms;
        long ss;
        long mm;
        bool isInLoop = false;

        int lapNumber = 1;

        public MainPageCS()
        {
            timeLabel = new Label
            {
                Text = "00'00\"000",
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 50,
            };

            lapList = new ListView
            {
                ItemsSource = App.lapTimes,
                ItemTemplate = new DataTemplate(typeof(LapCell)),
                HorizontalOptions = LayoutOptions.Center,
                SeparatorVisibility = SeparatorVisibility.None,
            };

            startButton = new Button
            {
                Text = "Start",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            startButton.Clicked += StartButton_Clicked;
            lapButton = new Button
            {
                Text = "Lap",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsEnabled = false,
            };
            lapButton.Clicked += LapButton_Clicked;

            Title = "Stopwatch";
            Content = new StackLayout
            {
                Padding = 10,
                Spacing = 20,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    timeLabel,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            startButton,
                            lapButton,
                        },
                    },
                    lapList,
                },
            };
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            if (startButton.Text.ToLower() == "start" || startButton.Text.ToLower() == "restart")
            {
                App.lapTimes.Clear();
                startButton.Text = "Stop";
                lapButton.IsEnabled = true;
                sw.Reset();
                sw.Start();
                isInLoop = true;

                Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                {
                    ms = sw.ElapsedMilliseconds;
                    ss = ms / 1000;
                    ms = ms % 1000;
                    mm = ss / 60;
                    ss = ss % 60;
                    timeLabel.Text = string.Format("{0:00}'{1:00}\"{2:000}", mm, ss, ms);
#if DEBUG
                    Debug.WriteLine(sw.ElapsedMilliseconds);
#endif
                    return isInLoop;
                });

            }
            else
            {
                startButton.Text = "Restart";
                lapButton.IsEnabled = false;
                sw.Stop();
                Debug.WriteLine(sw.ElapsedMilliseconds);

                isInLoop = false;
            }
        }

        private void LapButton_Clicked(object sender, EventArgs e)
        {
            if (sw.ElapsedMilliseconds > 0)
            {
                if (lapNumber == 1)
                {
                    ms = sw.ElapsedMilliseconds;
                    App.lapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = ms });
                    lw.Start();
                    lapNumber++;
                }
                else
                {
                    ms = lw.ElapsedMilliseconds;
                    App.lapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = ms });
                    lw.Restart();
                    lapNumber++;
                }
            }
        }

    }
}
