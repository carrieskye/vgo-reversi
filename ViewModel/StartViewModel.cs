using Cells;
using System;
using System.Collections.Generic;

namespace ViewModel
{
    public class StartViewModel
    {
        public Cell<int> Dimension { get; }
        public Cell<String> DimensionString { get; }
        public PlayerInfoViewModel Player1 { get; }
        public PlayerInfoViewModel Player2 { get; }

        public StartViewModel()
        {
            Player1 = new PlayerInfoViewModel("Player 1", "Black");
            Player2 = new PlayerInfoViewModel("Player 2", "White");
            Dimension = Cell.Create(8);
            DimensionString = Cell.Derive(Dimension, dim => dim.ToString() + " x " + dim.ToString());
        }

        public IEnumerable<PlayerInfoViewModel> Players
        {
            get
            {
                yield return Player1;
                yield return Player2;
            }
        }
    }

    public class PlayerInfoViewModel
    {
        public string DefaultName { get; }
        public Cell<string> Name { get; }
        public Cell<string> Color { get; }
        public ColorSelectionViewModel AllColors { get; }

        public PlayerInfoViewModel(string defaultName, string initialColor)
        {
            DefaultName = defaultName;
            Name = Cell.Create("");
            AllColors = new ColorSelectionViewModel(initialColor);
            Color = AllColors.ChosenColor;
        }
    }
}
