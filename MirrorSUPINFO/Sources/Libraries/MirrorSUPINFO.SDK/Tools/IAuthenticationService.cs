using System.Threading.Tasks;
using MirrorSUPINFO.SDK.Models;

namespace MirrorSUPINFO.SDK.Tools
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> AskAuthenticationAsync(string authenticationReason);
    }
}