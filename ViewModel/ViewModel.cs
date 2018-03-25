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
        public ReversiGame ReversiGame { get; private set; }
        public IList<BoardRowViewModel> Rows { get; private set; }

        public BoardViewModel()
        {
            this.ReversiGame = new ReversiGame(8, 8);
            Rows = Enumerable.Range(0, ReversiGame.Board.Height).Select(i => new BoardRowViewModel(this, i)).ToList().AsReadOnly();
        }

        public void Update(ReversiGame game)
        {
            this.ReversiGame = game;
            Rows = Enumerable.Range(0, ReversiGame.Board.Height).Select(i => new BoardRowViewModel(this, i)).ToList().AsReadOnly();
            foreach (var row in Rows)
            {
                foreach (var square in row.Squares)
                {
                    System.Diagnostics.Debug.Write(square.Owner);
                }
                System.Diagnostics.Debug.WriteLine("");
            }
            
        }

    }

    public class BoardRowViewModel
    {
        private readonly int rowNumber;

        public IList<BoardSquareViewModel> Squares { get; private set; }

        public BoardRowViewModel(BoardViewModel board, int rowNumber)
        {
            this.rowNumber = rowNumber;
            Squares = Enumerable.Range(0, board.ReversiGame.Board.Width).Select(i => new BoardSquareViewModel(board, rowNumber, i)).ToList().AsReadOnly();
        }
    }

    public class BoardSquareViewModel
    {
        private readonly Vector2D position;
        public Cell<Player> Owner { get; set; }
        public ICommand PutStoneCommand { get; }

        public BoardSquareViewModel(BoardViewModel board, int rowNumber, int columnNumber)
        {
            this.position = new Vector2D(rowNumber, columnNumber);
            Owner = Cell.Create(board.ReversiGame.Board[position]);
            PutStoneCommand = new PutStoneCommand(board, this, position);
        }
    }

    public class PutStoneCommand : ICommand
    {
        private readonly Vector2D position;

        public event EventHandler CanExecuteChanged;
        public BoardSquareViewModel Square;
        public BoardViewModel Board;

        public PutStoneCommand(BoardViewModel board, BoardSquareViewModel Square, Vector2D position)
        {
            this.Board = board;
            this.Square = Square;
            this.position = position;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Board.Update(Board.ReversiGame.PutStone(position));
        }
    }
}
