using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XF_Stopwatch.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        Models.Stopwatch stopwatch = new Models.Stopwatch();

        public MainPageViewModel()
        {
            stopwatch.PropertyChanged += (sender, e) =>
            {
                Debug.WriteLine($"Type = {sender.GetType()}, object = {sender.ToString()}.");
                Debug.WriteLine($"Name = {e.PropertyName}");
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
