using Cells;
using DataStructures;
using Model.Reversi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    public class ColorSelectionViewModel
    {
        public IList<ColorSelectionRowViewModel> Rows { get; }
        public List<string> Row1 = new List<string>{
            "Yellow", "Orange", "Red", "DarkRed"
        };
        public List<string> Row2 = new List<string>{
               "GreenYellow", "Turquoise","CornflowerBlue", "DarkOrchid"
            };

        public List<string> Row3 = new List<string>
            {
               "Peru", "White", "Gray", "Black"
        };
        public List<List<string>> Colors;

        public ColorSelectionViewModel(Cell<Player> player)
        {
            Colors = new List<List<string>>()
            {
                Row1, Row2, Row3
            };
            Rows = Enumerable.Range(0, Colors.Count).Select(i => new ColorSelectionRowViewModel(player, i, Colors[i])).ToList().AsReadOnly();
        }

    }

    public class ColorSelectionRowViewModel
    {
        private readonly int rowNumber;
        public IList<ColorSelectionSquareViewModel> Squares { get; }

        public ColorSelectionRowViewModel(Cell<Player> player, int rowNumber, List<string> rowColors)
        {
            this.rowNumber = rowNumber;
            Squares = Enumerable.Range(0, rowColors.Count).Select(i => new ColorSelectionSquareViewModel(player, rowNumber, i, rowColors[i])).ToList().AsReadOnly();
        }
    }

    public class ColorSelectionSquareViewModel
    {
        private readonly Vector2D position;
        public Cell<string> Color { get; set; }
        public ICommand SelectColorCommand { get; }

        public ColorSelectionSquareViewModel(Cell<Player> player, int rowNumber, int columnNumber, string color)
        {
            this.position = new Vector2D(rowNumber, columnNumber);
            Color = Cell.Create(color);
            SelectColorCommand = new SelectColorCommand(player, position);
        }

    }

    public class SelectColorCommand : ICommand
    {
        private readonly Vector2D position;
        public event EventHandler CanExecuteChanged;
        private readonly Cell<Player> player;

        public SelectColorCommand(Cell<Player> player, Vector2D position)
        {
            this.position = position;
            this.player = player;

            player.ValueChanged += () => CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            player.Value.Color = (string)parameter;
        }
    }
}
