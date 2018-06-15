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
        public Cell<bool> IsGameOver { get; }
        public Cell<Player> Winner { get; }
        public Cell<string> GameOverMessage { get; }
        public PlayerViewModel Player1 { get; }
        public PlayerViewModel Player2 { get; }

        public BoardViewModel(int dimension, string namePlayer1, string namePlayer2, string colorPlayer1, string colorPlayer2)
        {
            ReversiGame = Cell.Create(new ReversiGame(dimension, dimension));
            ReversiGame.Value.CurrentPlayer.Name = namePlayer1;
            ReversiGame.Value.CurrentPlayer.OtherPlayer.Name = namePlayer2;
            ReversiGame.Value.CurrentPlayer.Color = colorPlayer1;
            ReversiGame.Value.CurrentPlayer.OtherPlayer.Color = colorPlayer2;

            Player1 = new PlayerViewModel(this, ReversiGame.Value.CurrentPlayer, dimension);
            Player2 = new PlayerViewModel(this, ReversiGame.Value.CurrentPlayer.OtherPlayer, dimension);

            IsGameOver = Cell.Derive(ReversiGame, g => g.IsGameOver);
            Winner = Cell.Derive(ReversiGame, g => GetWinner(g));
            GameOverMessage = Cell.Derive(ReversiGame, g => CreateGameOverMessage(g));

            Rows = Enumerable.Range(0, ReversiGame.Value.Board.Height).Select(i => new BoardRowViewModel(ReversiGame, i)).ToList().AsReadOnly();
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

        public IEnumerable<PlayerViewModel> Players
        {
            get
            {
                yield return Player1;
                yield return Player2;
            }
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
            position = new Vector2D(rowNumber, columnNumber);
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

    public class PlayerViewModel
    {
        private readonly BoardViewModel parent;
        private readonly Player player;
        public Cell<int> Score { get; }
        public Cell<double> ScoreBar { get; }
        public String Name => player.Name;
        public String Color => player.Color;

        public PlayerViewModel(BoardViewModel parent, Player player, int dimension)
        {
            this.parent = parent;
            this.player = player;
            Score = Cell.Derive(this.parent.ReversiGame, g => g.Board.CountStones(player));
            ScoreBar = Cell.Derive(Score, s => ScoreBarSize(s, dimension));
        }

        private double ScoreBarSize(int points, int dimension)
        {
            return (double)points * ((dimension - 2) * 32) / (dimension * dimension);
        }
    }
}
