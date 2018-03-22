using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class BoardViewModel
    {
        public List<BoardRowViewModel> Rows;

        public BoardViewModel()
        {
            Rows = new List<BoardRowViewModel>(8);
        }
    }

    public class BoardRowViewModel
    {
        public List<BoardSquareViewModel> Squares;

        public BoardRowViewModel()
        {
            Squares = new List<BoardSquareViewModel>(8);
        }
    }

    public class BoardSquareViewModel
    {

    }
}
