using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static StoreMongo.AddGood;

namespace StoreMongo
{
    public class IntToStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string s = value as string;
                return int.Parse(s);
            }
            catch
            {
                return 0;
            }
        }
    }

    public class DoubleToStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            return double.Parse(s);
        }
    }

    public class Good_typesToVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var par = int.Parse(parameter as string);
            var gt = (int)value;
            return gt == par ? Visibility.Visible: Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var par = int.Parse(parameter as string);
            var vis = (Visibility)value;
            return (Good_types)par;
        }
    }
}
