using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Xamarin.Forms;

namespace XF_Stopwatch.Helpers
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;

            var ms = (long)value;

            var ss = ms / 1000;
            ms = ms % 1000;
            var mm = ss / 60;
            ss = ss % 60;
            return string.Format("{0:00}'{1:00}\"{2:000}", mm, ss, ms);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
