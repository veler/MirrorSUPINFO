using System;
using Windows.UI.Xaml.Navigation;

namespace MirrorSUPINFO.SDK.Tools
{
    public interface INavigationService
    {
        #region Properties

        bool CanGoBack { get; }

        object NavigationResult { get; }

        #endregion

        #region Events

        event NavigatedEventHandler Navigated;

        event NavigatedEventHandler NavigatedBack;

        #endregion

        #region Methods

        void Navigate(Type page, object[] arguments);

        void GoBack(object navigationResult = null);

        #endregion
    }
}