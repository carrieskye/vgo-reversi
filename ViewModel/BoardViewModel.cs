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
        public Cell<double> Player1ScoreBar { get; }
        public Cell<double> Player2ScoreBar { get; }
        public Cell<bool> IsGameOver { get; }
        public Cell<Player> Winner { get; }
        public Cell<string> GameOverMessage { get; }

        public BoardViewModel(int dimension, string namePlayer1, string namePlayer2, string colorPlayer1, string colorPlayer2)
        {
            this.ReversiGame = Cell.Create(new ReversiGame(dimension, dimension));
            this.ReversiGame.Value.CurrentPlayer.Name = namePlayer1;
            this.ReversiGame.Value.CurrentPlayer.OtherPlayer.Name = namePlayer2;
            this.ReversiGame.Value.CurrentPlayer.Color = colorPlayer1;
            this.ReversiGame.Value.CurrentPlayer.OtherPlayer.Color = colorPlayer2;

            this.Player1ScoreBar = Cell.Derive(ReversiGame, g => ScoreBarSize(g.Board.CountStones(g.FirstPlayer), dimension));
            this.Player2ScoreBar = Cell.Derive(ReversiGame, g => ScoreBarSize(g.Board.CountStones(g.FirstPlayer.OtherPlayer), dimension));

            this.IsGameOver = Cell.Derive(ReversiGame, g => g.IsGameOver);
            this.Winner = Cell.Derive(ReversiGame, g => GetWinner(g));
            this.GameOverMessage = Cell.Derive(ReversiGame, g => CreateGameOverMessage(g));

            Rows = Enumerable.Range(0, ReversiGame.Value.Board.Height).Select(i => new BoardRowViewModel(this.ReversiGame, i)).ToList().AsReadOnly();
        }

        private Player GetWinner(ReversiGame game)
        {
            int scoreCurrentPlayer = game.Board.CountStones(ReversiGame.Value.CurrentPlayer);
            int scoreOtherPlayer = game.Board.CountStones(ReversiGame.Value.CurrentPlayer.OtherPlayer);
            if (scoreCurrentPlayer > scoreOtherPlayer)
            {
                return game.CurrentPlayer;
            }
            else if (scoreCurrentPlayer < scoreOtherPlayer)
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
                int tieScore = game.Board.CountStones(ReversiGame.Value.CurrentPlayer);
                message += "Nobody wins: tie score of " + tieScore + " - " + tieScore + ".";

            }
            else
            {
                message += Winner.Value.Name + " won with " + game.Board.CountStones(GetWinner(game)) + " - " + game.Board.CountStones(GetWinner(game).OtherPlayer) + ".";
            }
            return message;
        }

        private double ScoreBarSize(int points, int dimension)
        {
            return (double)points * ((dimension - 2) * 32) / (dimension * dimension);
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
        public Cell<string> Type { get; set; }
        public ICommand PutStoneCommand { get; }

        public BoardSquareViewModel(Cell<ReversiGame> game, int rowNumber, int columnNumber)
        {
            this.position = new Vector2D(rowNumber, columnNumber);
            Owner = Cell.Derive(game, g => SetOwnerOrCandidate(g));
            Type = Cell.Derive(game, g => SetType(g));
            PutStoneCommand = new PutStoneCommand(game, position);
        }

        private Player SetOwnerOrCandidate(ReversiGame game)
        {
            if (game.Board[position] != null)
            {
                return game.Board[position];
            }
            else if (game.IsValidMove(position))
            {
                return game.CurrentPlayer;
            }
            else { return null; }
        }

        private string SetType(ReversiGame game)
        {
            return game.Board[position] != null ? "owner" : "candidate";
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
