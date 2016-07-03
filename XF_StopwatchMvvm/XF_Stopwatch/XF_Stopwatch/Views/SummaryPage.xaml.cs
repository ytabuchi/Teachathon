using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_Stopwatch.Views
{
    public partial class SummaryPage : ContentPage
    {
        public SummaryPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.SummaryPageViewModel();
        }
    }
}
