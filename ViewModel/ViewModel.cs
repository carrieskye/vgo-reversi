using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class BoardViewModel
    {
        private readonly IList<BoardRowViewModel> rows;

        public IList<BoardRowViewModel> Rows { get { return rows; } }

        public BoardViewModel()
        {
            rows = Enumerable.Range(1, 8).Select(_ => new BoardRowViewModel()).ToList().AsReadOnly();
        }
    }

    public class BoardRowViewModel
    {
        private readonly IList<BoardSquareViewModel> squares;

        public IList<BoardSquareViewModel> Squares { get { return squares; } }

        public BoardRowViewModel()
        {
            squares = Enumerable.Range(1, 8).Select(_ => new BoardSquareViewModel()).ToList().AsReadOnly();
        }
    }

    public class BoardSquareViewModel
    {

    }
}
