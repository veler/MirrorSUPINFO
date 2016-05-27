using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using MirrorSUPINFO.SDK.PathConverter;

namespace MirrorSUPINFO.Design.Controls
{
    public partial class IconButton : UserControl
    {
        PropertiesHolder propHolder;

        public IconButton()
        {
            propHolder = new PropertiesHolder();

            this.InitializeComponent();
            this.DataContext = propHolder;
        }

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(
            "ImageWidth", typeof(double), typeof(IconButton), new PropertyMetadata(16d)
        );

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(
            "ImageHeight", typeof(double), typeof(IconButton), new PropertyMetadata(16d)
        );

        //public string ImageKey
        //{
        //    get
        //    {
        //        var property = this.Resources[ImageKeyProperty].ToString();
        //        return Convert(property);
        //    }
        //    set
        //    {
        //        propHolder.pathText = this.Resources[value].ToString();
        //    }
        //}

        public string ImageKey
        {
            get
            {
                var property = this.Resources.MergedDictionaries[0][ImageKeyProperty].ToString();
                return Convert(property);
            }
            set
            {
                propHolder.pathText = this.Resources.MergedDictionaries[0][value].ToString();
            }
        }

        public static readonly DependencyProperty ImageKeyProperty = DependencyProperty.Register("ImageKey", typeof(string), typeof(IconButton), new PropertyMetadata(default(string)));

        private static string Convert(string value)
        {
            if (value == null)
            {
                return null;
            }

            var converter = new StringToPathGeometryConverter();
            var convertString = converter.Convert(value);
            var pathText = converter.ConvertBack(convertString);

            return pathText?.Length > 0 ? pathText : null;
        }


        public class PropertiesHolder : INotifyPropertyChanged
        {
            public string pathText
            {
                get { return _pathText; }
                set
                {
                    _pathText = value;
                    OnPropertyChanged("pathText");
                }
            }

            string _pathText = default(string);

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

            #endregion
        }


    }
}
