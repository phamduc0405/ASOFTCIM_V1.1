using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Threading.Tasks;
using System.Windows;

namespace ASOFTCIM.MVVM.ViewModels
{
    public class WaittingDisplayViewModel : IValueConverter,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double progress)
            {
                double angle = progress / 100.0 * 360.0;
                double radians = (Math.PI / 180.0) * angle;

                double centerX = 50;
                double centerY = 50;
                double radius = 50;

                double x = centerX + radius * Math.Sin(radians);
                double y = centerY - radius * Math.Cos(radians);

                return new Point(x, y);
            }
            return new Point(50, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
