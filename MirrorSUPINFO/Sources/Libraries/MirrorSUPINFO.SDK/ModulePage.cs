
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MirrorSUPINFO.SDK.Tools;

namespace MirrorSUPINFO.SDK
{
    public class ModulePage : UserControl
    {
        #region Properties

        public static readonly DependencyProperty ArgumentsProperty = DependencyProperty.Register("Arguments", typeof(object[]), typeof(ModulePage), new PropertyMetadata(null));

        public object[] Arguments
        {
            get { return (object[])GetValue(ArgumentsProperty); }
            set { SetValue(ArgumentsProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ModulePage), new PropertyMetadata(false, IsActivePropertyChangedCallback));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty ToolsProperty = DependencyProperty.Register("Tools", typeof(ITools), typeof(ModulePage), new PropertyMetadata(null));

        public ITools Tools
        {
            get { return (ITools)GetValue(ToolsProperty); }
            set { SetValue(ToolsProperty, value); }
        }

        #endregion

        #region Events

        public event EventHandler<bool> IsActiveChanged;

        #endregion

        #region Constructors

        public ModulePage(ITools tools, object[] arguments)
        {
            DataContext = null;
            Width = 1080;
            Height = 1590;
            Loaded += ApplicationPage_Loaded;
            Tools = tools;
            Arguments = arguments;
        }

        #endregion

        #region Handled Methods

        private void ApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ModulePageViewModelBase))
            {
                throw new InvalidCastException($"The DataContext must be of type {nameof(ModulePageViewModelBase)}.");
            }
        }

        #endregion

        #region Methods

        private static void IsActivePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = (ModulePage)dependencyObject;
            if (control.IsActiveChanged != null)
            {
                control.IsActiveChanged(control, control.IsActive);
            }
        }

        #endregion
    }
}
