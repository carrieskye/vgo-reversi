using DataStructures;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BoardViewModel board;

        public MainWindow()
        {
            InitializeComponent();
            const int WIDTH = 8;
            const int HEIGHT = 8;
            var grid = new Grid<int>(WIDTH, HEIGHT, p => p.X + p.Y * WIDTH);

            this.DataContext = grid;

            board = new BoardViewModel();
            Rows.DataContext = board;
        }
    }
}
