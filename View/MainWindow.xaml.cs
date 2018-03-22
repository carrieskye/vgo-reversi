using DataStructures;
using Model.Reversi;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            ReversiGame game = new ReversiGame(8, 8);

            board = new BoardViewModel(game.Board);

            this.DataContext = board;
        }
    }


    public class PlayerToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Transparant";
            }
            else
            {
                switch (value.ToString())
                {
                    case "B":
                        return "Black";
                    case "W":
                        return "White";
                    default:
                        return "Transparant";
                }
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
