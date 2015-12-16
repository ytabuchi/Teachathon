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
        Button startButton;
        Button lapButton;
        ListView lapList;
        Button resultButton;

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

            lapList = new ListView
            {
                ItemsSource = App.lapTimes,
                ItemTemplate = new DataTemplate(typeof(LapCell)), // Cell定義
                HorizontalOptions = LayoutOptions.Center,
                SeparatorVisibility = SeparatorVisibility.None,
            };

            resultButton = new Button
            {
                Text = "View Result",
                IsVisible = false,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Command = new Command(() => Navigation.PushAsync(new ResultPage(App.lapTimes))) //ResultPageにコレクションを渡してます
            };

            Title = "Stopwatch Event base";
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
                    resultButton,
                },
            };
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            if (startButton.Text.ToLower() == "start" || startButton.Text.ToLower() == "restart")
            {
                App.lapTimes.Clear(); // UWPで"System.ArgumentOutOfRangeException"が出ます？
                startButton.Text = "Stop";
                lapButton.IsEnabled = true;
                sw.Restart();
                isInLoop = true;

                // 10ミリ秒ごとにチェックされ、trueな限り継続
                Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                {
                    ms = sw.ElapsedMilliseconds;
                    ss = ms / 1000;
                    ms = ms % 1000;
                    mm = ss / 60;
                    ss = ss % 60;
                    timeLabel.Text = string.Format("{0:00}'{1:00}\"{2:000}", mm, ss, ms);

                    return isInLoop;
                });

            }
            else
            {
                startButton.Text = "Restart";
                lapButton.IsEnabled = false;
                resultButton.IsVisible = true;
                sw.Stop();
                lw.Stop();
                isInLoop = false;

                if (lapNumber == 1)
                    App.lapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = sw.ElapsedMilliseconds });
                else
                    App.lapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = lw.ElapsedMilliseconds });

                ms = sw.ElapsedMilliseconds;
                ss = ms / 1000;
                ms = ms % 1000;
                mm = ss / 60;
                ss = ss % 60;
                var alertTitle = string.Format("All time: {0:00}'{1:00}\"{2:000}", mm, ss, ms);

                var max = App.lapTimes.Max(i => i.LapTime);
                var min = App.lapTimes.Min(j => j.LapTime);

                DisplayAlert(alertTitle, string.Format("Max laptime: {0}ms\nMin laptime: {1}ms", max, min), "OK");
            }
        }

        private void LapButton_Clicked(object sender, EventArgs e)
        {
            if (sw.ElapsedMilliseconds > 0)
            {
                if (lapNumber == 1)
                    ms = sw.ElapsedMilliseconds;
                else
                    ms = lw.ElapsedMilliseconds;

                App.lapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = ms });
                lw.Restart();
                lapNumber++;
            }
        }
    }
}