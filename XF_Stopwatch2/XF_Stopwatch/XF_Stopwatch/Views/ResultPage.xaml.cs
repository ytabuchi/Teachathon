using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XF_Stopwatch.Models;

namespace XF_Stopwatch.Views
{
    public partial class ResultPage : ContentPage
    {
        public ResultPage(ObservableCollection<LapTime> laptime)
        {
            InitializeComponent();
            this.BindingContext = laptime;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
