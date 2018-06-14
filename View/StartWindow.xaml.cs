using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ViewModel;

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

        private void ShowColorsPlayer1(object sender, RoutedEventArgs e)
        {
            ColorSelectionPlayer1.IsOpen = true;
        }

        private void HideColorsPlayer1(object sender, RoutedEventArgs e)
        {
            ColorSelectionPlayer1.IsOpen = false;
        }

        private void ShowColorsPlayer2(object sender, RoutedEventArgs e)
        {
            ColorSelectionPlayer2.IsOpen = true;
        }

        private void HideColorsPlayer2(object sender, RoutedEventArgs e)
        {
            ColorSelectionPlayer2.IsOpen = false;
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            MainWindow gameWindow = new MainWindow(startViewModel.Dimension.Value, startViewModel.NamePlayer1.Value, startViewModel.NamePlayer2.Value, startViewModel.ColorPlayer1.Value, startViewModel.ColorPlayer2.Value);
            gameWindow.Show();
            this.Close();
        }
    }
}
