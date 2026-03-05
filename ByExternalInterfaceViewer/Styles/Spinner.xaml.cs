using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ByExternalInterfaceViewer.Styles
{
    public partial class Spinner : UserControl
    {
        public Spinner()
        {
            InitializeComponent();
        }

        // DependencyProperty per il colore dello spinner
        public static readonly DependencyProperty SpinnerColorProperty =
            DependencyProperty.Register(
                nameof(SpinnerColor),
                typeof(Brush),
                typeof(Spinner),
                new PropertyMetadata(Brushes.Blue)); // colore di default

        public Brush SpinnerColor
        {
            get { return (Brush)GetValue(SpinnerColorProperty); }
            set { SetValue(SpinnerColorProperty, value); }
        }
    }
}