using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ViewModel;
using System.Windows.Data;
using System;
using System.Globalization;
using System.Windows.Media;

namespace View
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private StartViewModel startViewModel;

        public StartWindow()
        {
            InitializeComponent();

            startViewModel = new StartViewModel();
            this.DataContext = startViewModel;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            ColorSelectionPlayer1.IsOpen = true;
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            ColorSelectionPlayer1.IsOpen = false;
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            MainWindow gameWindow = new MainWindow(startViewModel.Dimension.Value, startViewModel.NamePlayer1.Value, startViewModel.NamePlayer2.Value);
            gameWindow.Show();
            this.Close();
        }
    }

    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (string)value;
            BrushConverter brushConverter = new BrushConverter();
            return (Brush) brushConverter.ConvertFrom(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
