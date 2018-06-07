using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

        private void StartGame(object sender, RoutedEventArgs e)
        {
            MainWindow gameWindow = new MainWindow(startViewModel.Dimension.Value);
            gameWindow.Show();
            this.Close();
        }
    }
}
