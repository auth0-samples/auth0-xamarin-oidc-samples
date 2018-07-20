using System.Threading.Tasks;
using IdentityModel.OidcClient;

namespace FormsSample
{
    public interface IAuthenticationService
    {
        Task<LoginResult> Authenticate();
    }
}