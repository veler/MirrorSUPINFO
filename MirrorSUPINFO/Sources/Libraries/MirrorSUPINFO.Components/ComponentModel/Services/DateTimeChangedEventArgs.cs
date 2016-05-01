using System;

namespace MirrorSUPINFO.Components.ComponentModel.Services
{
    internal class DateTimeChangedEventArgs : EventArgs
    {
        public DateTime currentTime { get; set; }
    }
}