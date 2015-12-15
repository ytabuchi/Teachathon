using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XF_Stopwatch.ViewModels;

namespace XF_Stopwatch.Views
{
    public partial class MainPageXaml : ContentPage
    {
        public MainPageXaml()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<MainViewModel, string>(this, "Hi", (sender, arg) =>
            {
                DisplayAlert("Message Recieved", arg, "OK");
            });
        }
    }
}
