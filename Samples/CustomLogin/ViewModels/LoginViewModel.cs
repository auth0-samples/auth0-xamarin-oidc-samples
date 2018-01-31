using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;

namespace CustomLogin
{
    public class LoginViewModel : BaseViewModel
    {
        private const string Auth0Domain = "jerrie.auth0.com";
        private const string Auth0ClientId = "vV9twaySQzfGesS9Qs6gOgqDsYDdgoKE";
        private const string Auth0ClientSecret = "Z1RBGRi3zcmHROfdVgn1Fctv4dcGVNsIrb4feHp0kA9f8MYGZRZqXWCNMTd0Ipsa";

        IAuthenticationApiClient authenticationApiClient;
        INavigation navigation;

        public string Username { get; set; }
        public string Password { get; set; }

        public Command LoginCommand { get; set; }
        public Command SignUpCommand { get; set; }

        public LoginViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            authenticationApiClient = new AuthenticationApiClient(Auth0Domain);
                
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            SignUpCommand = new Command(async () => await ExecuteSignUpCommand());
        }

        private async Task ExecuteLoginCommand()
        {
            var tokenResponse = await authenticationApiClient.GetTokenAsync(
                new ResourceOwnerTokenRequest
                {
                    Realm = "Username-Password-Authentication",
                    Audience = "https://quickstarts/api",
                    Scope = "openid profile",
                    ClientId = Auth0ClientId,
                    ClientSecret = Auth0ClientSecret,
                    Username = Username,
                    Password = Password
                });

            await navigation.PopModalAsync();
        }

        private async Task ExecuteSignUpCommand()
        {
        }
    }
}
