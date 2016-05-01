namespace MirrorSUPINFO.SDK.Models
{
    public sealed class AuthenticationResult
    {
        #region Properties

        public string Login { get; set; }

        public string Password { get; set; }

        #endregion

        #region Constructors

        public AuthenticationResult(string login, string password)
        {
            Login = login;
            Password = password;
        }

        #endregion
    }
}
