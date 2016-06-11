namespace MirrorSUPINFO.SDK.Models
{
    public class Notification
    {
        #region Properties

        public string Title { get; private set; }

        public string Description { get; private set; }

        #endregion

        #region Constructors

        public Notification(string title, string description)
        {
            Title = title;
            Description = description;
        }

        #endregion
    }
}
