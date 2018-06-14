using Cells;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using ViewModel;

namespace ViewModel
{
    public class StartViewModel
    {
        public Cell<int> Dimension { get; }
        public Cell<string> NamePlayer1 { get; }
        public Cell<string> NamePlayer2 { get; }
        public Cell<string> ColorPlayer1 { get; }
        public Cell<string> ColorPlayer2 { get; }
        public Cell<String> DimensionString { get; }

        public StartViewModel()
        {
            this.Dimension = Cell.Create(8);
            this.NamePlayer1 = Cell.Create("Player 1");
            this.NamePlayer2 = Cell.Create("Player 2");
            this.ColorPlayer1 = Cell.Create("Black");
            this.ColorPlayer2 = Cell.Create("White");
            this.DimensionString = Cell.Derive(Dimension, dim => getDimensionString(dim));
        }

        private String getDimensionString(int dim)
        {
            return dim.ToString() + " x " + dim.ToString();
        }
    }

}
