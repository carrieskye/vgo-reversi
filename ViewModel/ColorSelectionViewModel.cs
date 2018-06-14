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
        public List<string> Row1, Row2, Row3;
        public List<List<string>> Colors;
        public Cell<string> ChosenColor;

        public ColorSelectionViewModel(string initialColor)
        {
            InitializeColors();
            ChosenColor = Cell.Create(initialColor);
            Rows = Enumerable.Range(0, Colors.Count).Select(i => new ColorSelectionRowViewModel(ChosenColor, i, Colors[i])).ToList().AsReadOnly();
        }

        private void InitializeColors()
        {
            Row1 = new List<string> { "Yellow", "Orange", "Red", "DarkRed" };
            Row2 = new List<string> { "GreenYellow", "Turquoise", "CornflowerBlue", "DarkOrchid" };
            Row3 = new List<string> { "Peru", "White", "Gray", "Black" };
            Colors = new List<List<string>>() { Row1, Row2, Row3 };
        }
    }

    public class ColorSelectionRowViewModel
    {
        private readonly int rowNumber;
        public IList<ColorSelectionSquareViewModel> Squares { get; }

        public ColorSelectionRowViewModel(Cell<string> chosenColor, int rowNumber, List<string> rowColors)
        {
            this.rowNumber = rowNumber;
            Squares = Enumerable.Range(0, rowColors.Count).Select(i => new ColorSelectionSquareViewModel(chosenColor, rowNumber, i, rowColors[i])).ToList().AsReadOnly();
        }
    }

    public class ColorSelectionSquareViewModel
    {
        private readonly Vector2D position;
        public Cell<string> Color { get; set; }
        public ICommand SelectColorCommand { get; }

        public ColorSelectionSquareViewModel(Cell<string> chosenColor, int rowNumber, int columnNumber, string color)
        {
            this.position = new Vector2D(rowNumber, columnNumber);
            Color = Cell.Create(color);
            SelectColorCommand = new SelectColorCommand(chosenColor, color);
        }
    }

    public class SelectColorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Cell<string> chosenColor;
        private readonly string color;

        public SelectColorCommand(Cell<string> chosenColor, string color)
        {
            this.chosenColor = chosenColor;
            this.color = color;

            chosenColor.ValueChanged += () => CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            chosenColor.Value = color;
        }
    }
}
