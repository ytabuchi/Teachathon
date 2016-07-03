using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_Stopwatch.Views
{
    public partial class StopwatchPage : ContentPage
    {
        public StopwatchPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.StopwatchPageViewModel();

            MessagingCenter.Subscribe<ViewModels.StopwatchPageViewModel, string>(this, "ShowDialog", async (s, e) =>
            {
                var res = await DisplayAlert("Total Time", $"Total time is {e}.\nView summary page?", "Yes", "Cancel");
                if (res)
                    await Navigation.PushAsync(new SummaryPage());
                else
                    this.BindingContext = new ViewModels.StopwatchPageViewModel(); 
            });
        }
    }
}
