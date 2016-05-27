using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace MirrorSUPINFO.Design.Controls
{
    public sealed partial class ListView : UserControl
    {
        public ListView()
        {
            this.InitializeComponent();

            List<Notifications> items = new List<Notifications>();
            items.Add(new Notifications() { Icon = "Bus", Content = "Notification 1" });
            items.Add(new Notifications() { Icon = "Bus", Content = "Notification 2" });
            items.Add(new Notifications() { Icon = "Bus", Content = "Notification 3" });
            listView.ItemsSource = items;

            this.DataContext = items;
        }
    }

    public class Notifications
    {
        public string Icon { get; set; }

        public string Content { get; set; }
    }
}
