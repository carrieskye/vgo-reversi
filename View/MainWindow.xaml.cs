using Model.Reversi;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BoardViewModel board;

        public MainWindow(int dimension, string namePlayer1, string namePlayer2)
        {
            InitializeComponent();
            this.Height = dimension * 32 + 160;
            this.Width = dimension * 32 + 100;
            if (this.Width < 228)
            {
                this.Width = 228;
            }

            board = new BoardViewModel(dimension, namePlayer1, namePlayer2);
            this.DataContext = board;
        }
    }


    public class PlayerToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var player = (Player)value;

            if (player == Player.BLACK)
            {
                return Brushes.Black;
            }
            else if (player == Player.WHITE)
            {
                return Brushes.White;
            }
            else
            {
                return Brushes.Transparent;
            }
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

            if (type.Equals("owner"))
            {
                return 1;
            }
            else if (type.Equals("candidate"))
            {
                return 0.4;
            }
            else
            {
                return 0;
            }
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
            if (gameOver == true)
            {
                return "Visible";
            }
            else
            {
                return "Collapsed";
            }
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
            if (gameOver == true)
            {
                return "Collapsed";
            }
            else
            {
                return "Visible";
            }
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
            var winner = (Player)value;
            if (winner == null)
            {
                return "Collapsed";
            }
            else
            {
                return "Visible";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
