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
        Label switchLabel;
        Switch decimalSwitch;
        ListView lapList;

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

            switchLabel = new Label
            {
                Text = "Show decimal point",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            decimalSwitch = new Switch
            {
                IsToggled = true,
            };
            decimalSwitch.Toggled += DecimalSwitch_Toggled;

            lapList = new ListView
            {
                ItemsSource = App.lapTimes,
                ItemTemplate = new DataTemplate(typeof(LapCell)), // Cell定義
                HorizontalOptions = LayoutOptions.Center,
                SeparatorVisibility = SeparatorVisibility.None,
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
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            switchLabel,
                            decimalSwitch,
                        },
                    },
                    lapList,
                },
            };
        }

        private async void StartButton_Clicked(object sender, EventArgs e)
        {
            if (startButton.Text.ToLower() == "start" || startButton.Text.ToLower() == "restart")
            {
                App.lapTimes.Clear(); // UWPで"System.ArgumentOutOfRangeException"が出ます
                lapNumber = 1;
                startButton.Text = "Stop";
                lapButton.IsEnabled = true;
                sw.Restart();
                isInLoop = true;

                // 10ミリ秒ごとにチェックされ、trueな限り継続
                Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                {
                    timeLabel.Text = App.ChangeFormat(sw.ElapsedMilliseconds);

                    return isInLoop;
                });
            }
            else
            {
                startButton.Text = "Restart";
                lapButton.IsEnabled = false;
                sw.Stop();
                lw.Stop();
                isInLoop = false;

                if (lapNumber == 1)
                    App.lapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = sw.ElapsedMilliseconds });
                else
                    App.lapTimes.Add(new LapTimes { LapNumber = lapNumber, LapTime = lw.ElapsedMilliseconds });

                ms = sw.ElapsedMilliseconds;

                var max = App.ChangeFormat(App.lapTimes.Max(i => i.LapTime));
                var min = App.ChangeFormat(App.lapTimes.Min(j => j.LapTime));
                var res = await DisplayAlert(App.ChangeFormat(ms), string.Format("Max laptime: {0}\nMin laptime: {1}\n\nShow all lap result?", max, min), "Yes", "No");

                if (res)
                    await Navigation.PushAsync(new ResultPage(App.lapTimes));
                else
                {
                    App.lapTimes.Clear();
                    lapNumber = 1;
                    startButton.Text = "Start";
                    timeLabel.Text = "00'00\"000";
                }
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

        private void DecimalSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            App.isShowed = ((Switch)sender).IsToggled;

            // ここに多分各種コントロールのフォーマットを変更する処理を追加する？

        }

        //private string ChangeFormat(long ms)
        //{
        //    ss = ms / 1000;
        //    ms = ms % 1000;
        //    mm = ss / 60;
        //    ss = ss % 60;

        //    if (App.isShowed)
        //        return string.Format("{0:00}'{1:00}\"{2:000}", mm, ss, ms);
        //    else
        //        return string.Format("{0:00}'{1:00}\"", mm, ss);
        //}
    }
}