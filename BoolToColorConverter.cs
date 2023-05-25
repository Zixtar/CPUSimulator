using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CPUSimulator
{
    internal class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var used = (bool)value;
            if(used)
                return Brushes.OrangeRed;
            else
            {
                return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as Brush;
            if (brush == Brushes.Black)
            {
                return false;
            }
            else
            {
                return true;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
