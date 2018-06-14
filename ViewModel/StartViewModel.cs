using Cells;
using System;

namespace ViewModel
{
    public class StartViewModel
    {
        public string Text { get; }
        public Cell<int> Dimension { get; }
        public Cell<string> NamePlayer1 { get; }
        public Cell<string> NamePlayer2 { get; }
        public Cell<string> ColorPlayer1 { get; }
        public Cell<string> ColorPlayer2 { get; }
        public Cell<String> DimensionString { get; }
        public ColorSelectionViewModel ColorSelectionPlayer1 { get; }
        public ColorSelectionViewModel ColorSelectionPlayer2 { get; }

        public StartViewModel()
        {
            this.NamePlayer1 = Cell.Create("");
            this.NamePlayer2 = Cell.Create("");

            this.ColorSelectionPlayer1 = new ColorSelectionViewModel("Black");
            this.ColorSelectionPlayer2 = new ColorSelectionViewModel("White");
            this.ColorPlayer1 = ColorSelectionPlayer1.ChosenColor;
            this.ColorPlayer2 = ColorSelectionPlayer2.ChosenColor;

            this.Dimension = Cell.Create(8);
            this.DimensionString = Cell.Derive(Dimension, dim => dim.ToString() + " x " + dim.ToString());
        }
    }
}
