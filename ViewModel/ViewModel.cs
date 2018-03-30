using Cells;
using DataStructures;
using Model.Reversi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    public class BoardViewModel
    {
        public Cell<ReversiGame> ReversiGame { get; private set; }
        public Cell<ReversiBoard> ReversiBoard { get; private set; }
        public IList<BoardRowViewModel> Rows { get; private set; }

        public BoardViewModel()
        {
            this.ReversiGame = Cell.Create(new ReversiGame(8, 8));
            this.ReversiBoard = Cell.Derive(ReversiGame, g => g.Board);
            Rows = Enumerable.Range(0, ReversiGame.Value.Board.Height).Select(i => new BoardRowViewModel(ReversiBoard, i)).ToList().AsReadOnly();
        }
        
        public void Update(ReversiGame game)
        {
            this.ReversiGame.Value = game;
            
            for (int i = 0; i < ReversiGame.Value.Board.Height; i++)
            {
                for (int j = 0; j < ReversiGame.Value.Board.Width; j++)
                {
                    var position = new Vector2D(i, j);
                    Rows[i].Squares[j].Owner.Value = ReversiGame.Value.Board[position];
                }
            }
        }

    }

    public class BoardRowViewModel
    {
        private readonly int rowNumber;

        public IList<BoardSquareViewModel> Squares { get; private set; }

        public BoardRowViewModel(Cell<ReversiBoard> board, int rowNumber)
        {
            this.rowNumber = rowNumber;
            Squares = Enumerable.Range(0, board.Value.Width).Select(i => new BoardSquareViewModel(board, rowNumber, i)).ToList().AsReadOnly();
        }
    }

    public class BoardSquareViewModel
    {
        private readonly Vector2D position;
        public Cell<Player> Owner { get; set; }
        public ICommand PutStoneCommand { get; }

        public BoardSquareViewModel(Cell<ReversiBoard> board, int rowNumber, int columnNumber)
        {
            this.position = new Vector2D(rowNumber, columnNumber);
            Owner = Cell.Derive(board, b => b[position]);
            PutStoneCommand = new PutStoneCommand(Owner, position);
        }
    }

    public class PutStoneCommand : ICommand
    {
        private readonly Vector2D position;
        public Cell<Player> Owner;
        public event EventHandler CanExecuteChanged;
        public BoardViewModel Board;

        public PutStoneCommand(Cell<Player> Owner, Vector2D position)
        {
            this.position = position;

            Owner.ValueChanged += () => CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
            Board.Update(Board.ReversiGame.Value.PutStone(position));
        }
    }
}
