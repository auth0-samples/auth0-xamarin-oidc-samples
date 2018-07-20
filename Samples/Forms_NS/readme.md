This sample application demonstrates how to use the Auth0 OIDC Client in a Xamarin Forms application when using .NET Standard. You will need to refer to the normal [Xamarin Quickstart](https://auth0.com/docs/quickstart/native/xamarin) to see how to configure the configure the Callback URL and also how to handle the callback URL in both iOS and Android. For UWP, you will need to refer to the [UWP Quickstart](https://auth0.com/docs/quickstart/native/windows-uwp-csharp).

1. Ensure that you have installed the correct Nuget packages for each of your platforms as per the Quickstarts linked to above. You will also need to add the `Auth0.OidcClient.Core` Nuget package to the .NET Standard library which contains your Xamarin Forms pages.

2. Next, in the .NET Standard project, declare an `IAuthenticationService` interface with a single `Authenticate` method.

    ```csharp
    public interface IAuthenticationService
    {
        Task<LoginResult> Authenticate();
    }
    ```
    
3. This interface should then be implemented for each of the individual platforms. Take note to also specify the `DependencyAttribute` for the Xamarin DI to work:

    ```csharp
    [assembly: Dependency(typeof(AuthenticationService))]
    
    namespace Your_Namespace
    {
        public class AuthenticationService : IAuthenticationService
        {
            private Auth0Client _auth0Client;
    
            public AuthenticationService()
            {
                _auth0Client = new Auth0Client(new Auth0ClientOptions
                {
                    Domain = "your domain",
                    ClientId = "your client id"
                });
            }
    
            public Task<LoginResult> Authenticate()
            {
                return _auth0Client.LoginAsync();
            }
        }
    }
    ```
    
4. Finally, you can get an instance of `IAuthenticationService` inside your .NET Standard DLL, and call the `Authenticate` method to invoke the Auth0 OIDC Client:

    ```csharp
    var authenticationService = DependencyService.Get<IAuthenticationService>();
    var loginResult = await authenticationService.Authenticate();
    ```
    