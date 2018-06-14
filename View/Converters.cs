using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace View
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (string)value;
            BrushConverter brushConverter = new BrushConverter();
            return (Brush)brushConverter.ConvertFrom(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TypeToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string)value;
            if (type.Equals("owner")) { return 1; }
            else if (type.Equals("candidate")) { return 0.4; }
            else { return 0; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GameOverToGameOverVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gameOver = (bool)value;
            if (gameOver == true) { return "Visible"; }
            else { return "Collapsed"; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GameOverToScoreVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gameOver = (bool)value;
            if (gameOver == true) { return "Collapsed"; }
            else { return "Visible"; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WinnerToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return "Collapsed"; }
            else { return "Visible"; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NameAndColorValidationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string name1 = (string)values[0];
            string name2 = (string)values[1];
            string color1 = (string)values[2];
            string color2 = (string)values[3];
            if (string.IsNullOrEmpty(name1.Trim()) || string.IsNullOrEmpty(name2.Trim())) return false;
            if (name1.Equals(name2) || color1.Equals(color2)) return false;
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
