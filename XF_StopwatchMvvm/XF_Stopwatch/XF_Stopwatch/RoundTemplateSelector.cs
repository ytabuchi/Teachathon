using System;
using Xamarin.Forms;

namespace XF_Stopwatch
{
    public class RoundTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DecimalTemplate { get; set; }
        public DataTemplate RoundedTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //            return ((Person)item).Age <= 40 ? Youngemplate : OldTemplate;
            return null;
        }
    }
}

