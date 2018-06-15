using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for ScoreBarControl.xaml
    /// </summary>
    public partial class ScoreBarControl : UserControl
    {
        public ScoreBarControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register("Player", typeof(string), typeof(ScoreBarControl), new PropertyMetadata(""));
        public string Player
        {
            get { return (string)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(string), typeof(ScoreBarControl), new PropertyMetadata(""));
        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(ScoreBarControl), new PropertyMetadata(0));
        public int Score
        {
            get { return (int)GetValue(ScoreProperty); }
            set { SetValue(ScoreProperty, value); }
        }

        public static readonly DependencyProperty ScoreBarProperty = DependencyProperty.Register("ScoreBar", typeof(double), typeof(ScoreBarControl), new PropertyMetadata(0.0));
        public double ScoreBar
        {
            get { return (double)GetValue(ScoreBarProperty); }
            set { SetValue(ScoreBarProperty, value); }
        }
    }
}
