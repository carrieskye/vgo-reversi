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

        public MainWindow(int dimension, string namePlayer1, string namePlayer2, string colorPlayer1, string colorPlayer2)
        {
            InitializeComponent();
            this.Height = dimension * 32 + 160;
            this.Width = dimension * 32 + 100;
            if (this.Width < 228) { this.Width = 228; }

            board = new BoardViewModel(dimension, namePlayer1, namePlayer2, colorPlayer1, colorPlayer2);
            this.DataContext = board;
        }
    }
}
