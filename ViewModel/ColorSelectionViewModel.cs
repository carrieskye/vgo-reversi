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
            InitializeColors(new List<string> { "Yellow", "Orange", "Red", "DarkRed", "GreenYellow", "Turquoise", "CornflowerBlue", "DarkOrchid", "Peru", "White", "Gray", "Black" });
            ChosenColor = Cell.Create(initialColor);
            Rows = Enumerable.Range(0, Colors.Count).Select(i => new ColorSelectionRowViewModel(ChosenColor, i, Colors[i])).ToList().AsReadOnly();
        }

        private void InitializeColors(List<string> listOfColors)
        {
            int width = 1;
            while (width * width < listOfColors.Count) width += 1;
            int height = listOfColors.Count / width;
            if (listOfColors.Count % width != 0) height += 1;
            Colors = new List<List<string>>();
            for (int i = 0; i < height; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < width; j++)
                {
                    if (i * width + j < listOfColors.Count)
                    {
                        row.Add(listOfColors[i * width + j]);
                    }
                }
                Colors.Add(row);
            }
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
            position = new Vector2D(rowNumber, columnNumber);
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
