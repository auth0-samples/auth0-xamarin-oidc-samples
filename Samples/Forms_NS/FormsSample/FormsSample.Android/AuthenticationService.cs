using System.Threading.Tasks;
using Auth0.OidcClient;
using FormsSample.Droid;
using IdentityModel.OidcClient;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticationService))]

namespace FormsSample.Droid
{
    public class AuthenticationService : IAuthenticationService
    {
        private Auth0Client _auth0Client;

        public AuthenticationService()
        {
            _auth0Client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = AuthenticationConfig.Domain,
                ClientId = AuthenticationConfig.ClientId
            });
        }

        public Task<LoginResult> Authenticate()
        {
            return _auth0Client.LoginAsync();
        }
    }
}