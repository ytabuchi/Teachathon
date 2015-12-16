using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using XF_Stopwatch.Helpers;

namespace XF_Stopwatch.Views
{
    public class LapCell : ViewCell
    {
        public LapCell()
        {
            var numLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
            };
            numLabel.SetBinding(Label.TextProperty, "LapNumber");

            var lapLabel = new Label
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
            };
            lapLabel.SetBinding(Label.TextProperty, new Binding("LapTime", converter: new TimeConverter()));

            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 20,
                Children =
                {
                    numLabel,
                    lapLabel,
                },
            };
        }
    }
}
