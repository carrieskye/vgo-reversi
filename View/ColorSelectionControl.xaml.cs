using Cells;
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
    /// Interaction logic for ColorSelectionControl.xaml
    /// </summary>
    public partial class ColorSelectionControl : UserControl
    {
        private ColorSelectionViewModel colorViewModel;

        public ColorSelectionControl()
        {
            InitializeComponent();

            //colorViewModel = new ColorSelectionViewModel();
            //this.Height = colorViewModel.Colors.Count * 30 + 50;
            //this.Width = colorViewModel.Row1.Count * 30 + 50;
            //this.DataContext = colorViewModel;
        }
    }
}
