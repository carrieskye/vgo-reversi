using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BoardViewModel board;

        public MainWindow(int dimension, PlayerInfoViewModel player1, PlayerInfoViewModel player2)
        {
            InitializeComponent();
            Height = dimension * 32 + 160;
            Width = dimension * 32 + 100;
            if (Width < 228) { Width = 228; }

            board = new BoardViewModel(dimension, player1, player2);
            DataContext = board;
        }
    }
}
