using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI;
using JobChannel.Mobile.Domain.BO;

namespace JobChannel.Mobile
{
    public class TypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = targetType switch
            {
                var c when c == typeof(Region) => Colors.LightBlue,
                var c when c == typeof(Contract) => Colors.LightGreen,
                var c when c == typeof(Job) => Colors.LightYellow,
                _ => Colors.Black
            };

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
