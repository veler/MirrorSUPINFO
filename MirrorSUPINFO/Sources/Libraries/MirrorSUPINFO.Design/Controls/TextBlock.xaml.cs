using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MirrorSUPINFO.Design.Controls
{
    public partial class TextBlock : UserControl
    {
        public TextBlock()
        {
            this.InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextContent); }
            set { SetValue(TextContent, value); }
        }

        public static readonly DependencyProperty TextContent = DependencyProperty.Register("Text", typeof(string), typeof(TextBlock), new PropertyMetadata(null));
    }
}
