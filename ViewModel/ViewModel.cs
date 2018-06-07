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
        public Cell<ReversiGame> ReversiGame { get; }
        public IList<BoardRowViewModel> Rows { get; }
        public Cell<int> ScoreCurrentPlayer { get; }
        public Cell<int> ScoreOtherPlayer { get; }
        public Cell<bool> IsGameOver { get; }
        public Cell<Player> Winner { get; }
        public Cell<string> GameOverMessage { get; }

        public BoardViewModel(int dimension)
        {
            this.ReversiGame = Cell.Create(new ReversiGame(dimension, dimension));
            this.ScoreCurrentPlayer = Cell.Derive(ReversiGame, g => g.Board.CountStones(g.CurrentPlayer));
            this.ScoreOtherPlayer = Cell.Derive(ReversiGame, g => g.Board.CountStones(g.CurrentPlayer.OtherPlayer));
            this.IsGameOver = Cell.Derive(ReversiGame, g => g.IsGameOver);
            this.Winner = Cell.Derive(ReversiGame, g => GetWinner(g));
            this.GameOverMessage = Cell.Derive(ReversiGame, g => CreateGameOverMessage(g));
            Rows = Enumerable.Range(0, ReversiGame.Value.Board.Height).Select(i => new BoardRowViewModel(this.ReversiGame, i)).ToList().AsReadOnly();
        }

        private Player GetWinner(ReversiGame game)
        {
            if (ScoreCurrentPlayer.Value > ScoreOtherPlayer.Value)
            {
                return game.CurrentPlayer;
            }
            else if (ScoreCurrentPlayer.Value < ScoreOtherPlayer.Value)
            {
                return game.CurrentPlayer.OtherPlayer;
            }
            return null;
        }

        private String CreateGameOverMessage(ReversiGame game)
        {
            string message = "";
            if (GetWinner(game) == null)
            {
                message += "Tie score of " + ScoreCurrentPlayer.Value + " - " + ScoreOtherPlayer.Value;
                
            }else
            {
                message += "won with " + game.Board.CountStones(GetWinner(game)) + " - " + game.Board.CountStones(GetWinner(game).OtherPlayer);
            }
            return message;
        }
    }

    public class BoardRowViewModel
    {
        private readonly int rowNumber;
        public IList<BoardSquareViewModel> Squares { get; }

        public BoardRowViewModel(Cell<ReversiGame> game, int rowNumber)
        {
            this.rowNumber = rowNumber;
            Squares = Enumerable.Range(0, game.Value.Board.Width).Select(i => new BoardSquareViewModel(game, rowNumber, i)).ToList().AsReadOnly();
        }
    }

    public class BoardSquareViewModel
    {
        private readonly Vector2D position;
        public Cell<Player> Owner { get; set; }
        public ICommand PutStoneCommand { get; }

        public BoardSquareViewModel(Cell<ReversiGame> game, int rowNumber, int columnNumber)
        {
            this.position = new Vector2D(rowNumber, columnNumber);
            Owner = Cell.Derive(game, g => g.Board[position]);
            PutStoneCommand = new PutStoneCommand(game, position);
        }
    }

    public class PutStoneCommand : ICommand
    {
        private readonly Vector2D position;
        public event EventHandler CanExecuteChanged;
        private readonly Cell<ReversiGame> game;

        public PutStoneCommand(Cell<ReversiGame> game, Vector2D position)
        {
            this.position = position;
            this.game = game;

            game.ValueChanged += () => CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return game.Value.IsValidMove(position) && !game.Value.IsGameOver;
        }

        public void Execute(object parameter)
        {
            game.Value = game.Value.PutStone(position);
        }
    }
}
