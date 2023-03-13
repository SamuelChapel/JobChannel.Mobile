using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI;
using JobChannel.Mobile.Domain.BO;

namespace JobChannel.Mobile
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //DateTimeOffset? res;
            //if (value is null)
            //    res = null;
            //else
            //    res = new DateTimeOffset(((DateTime)value).ToUniversalTime());
            //return res;
            return value is null ? null : new DateTimeOffset?(((DateTime)value).ToUniversalTime());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //DateTime? res;
            //if (value is null)
            //    res = null;
            //else
            //    res = ((DateTimeOffset)value).DateTime;
            //return res;
            return value is null ? null : ((DateTimeOffset?)value)?.DateTime;
        }
    }
}
