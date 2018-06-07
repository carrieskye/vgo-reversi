using Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class StartViewModel
    {
        public Cell<int> Dimension { get; }
        public Cell<String> DimensionString { get; }

        public StartViewModel()
        {
            this.Dimension = Cell.Create(8);
            this.DimensionString = Cell.Derive(Dimension, dim => getDimensionString(dim));
        }

        private String getDimensionString(int dim)
        {
            return dim.ToString() + " x " + dim.ToString();
        }
    }

  
}
