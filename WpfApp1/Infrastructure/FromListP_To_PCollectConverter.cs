using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp1.Infrastructure
{
    public class FromListP_To_PCollectConverter : IValueConverter
    {
            public object Convert(object value, Type targetType,
                object parameter, CultureInfo culture)
            {
            var collection = new PointCollection();
            var list = value as List<Point>;

            foreach (var item in list)
            {
                collection.Add(item);
            }
            return collection;
            }

            public object ConvertBack(object value, Type targetType,
                object parameter, CultureInfo culture)
            {
            throw new NotImplementedException(); 
            }
        
    }
}
