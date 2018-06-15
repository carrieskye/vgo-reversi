using System.Windows;
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
            DataContext = startViewModel;
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
            MainWindow gameWindow = new MainWindow(startViewModel.Dimension.Value, startViewModel.Player1, startViewModel.Player2);
            gameWindow.Show();
            Close();
        }
    }
}
