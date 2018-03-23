using Cells;
using DataStructures;
using Model.Reversi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ViewModel
{
    public class BoardViewModel
    {
        private readonly IList<BoardRowViewModel> rows;
        private readonly ReversiGame reversiGame;

        public IList<BoardRowViewModel> Rows { get { return rows; } }

        public BoardViewModel(ReversiGame reversiGame)
        {
            this.reversiGame = reversiGame;
            rows = Enumerable.Range(0, reversiGame.Board.Height).Select(i => new BoardRowViewModel(reversiGame, i)).ToList().AsReadOnly();
        }
    }

    public class BoardRowViewModel
    {
        private readonly IList<BoardSquareViewModel> squares;
        private readonly ReversiGame reversiGame;
        private readonly int rowNumber;

        public IList<BoardSquareViewModel> Squares { get { return squares; } }

        public BoardRowViewModel(ReversiGame reversiGame, int rowNumber)
        {
            this.reversiGame = reversiGame;
            this.rowNumber = rowNumber;
            squares = Enumerable.Range(0, reversiGame.Board.Width).Select(i => new BoardSquareViewModel(reversiGame, rowNumber, i)).ToList().AsReadOnly();
        }
    }

    public class BoardSquareViewModel
    {
        private readonly ReversiGame reversiGame;
        private readonly Vector2D position;
        public Cell<Player> Owner { get; set; }
        public ICommand PutStoneCommand { get; }

        public BoardSquareViewModel(ReversiGame reversiGame, int rowNumber, int columnNumber)
        {
            this.reversiGame = reversiGame;
            this.position = new Vector2D(rowNumber, columnNumber);
            Owner = Cell.Create(reversiGame.Board[position]);
            PutStoneCommand = new PutStoneCommand(reversiGame, this, position);
        }
    }

    public class PutStoneCommand : ICommand
    {
        private readonly Vector2D position;

        public event EventHandler CanExecuteChanged;
        public BoardSquareViewModel Square;
        public ReversiGame reversiGame;

        public PutStoneCommand(ReversiGame reversiGame, BoardSquareViewModel Square, Vector2D position)
        {
            this.reversiGame = reversiGame;
            this.Square = Square;
            this.position = position;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            reversiGame = reversiGame.PutStone(position);
        }
    }
}
