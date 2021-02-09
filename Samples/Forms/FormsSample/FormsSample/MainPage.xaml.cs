using System;
using System.Text;
using Auth0.OidcClient;
using Xamarin.Forms;

namespace FormsSample
{
	public partial class MainPage : ContentPage
	{
	    private readonly IAuth0Client _auth0Client;

	    public MainPage()
	    {
	        InitializeComponent();

	        _auth0Client = new Auth0Client(new Auth0ClientOptions
	        {
	            Domain = "{DOMAIN}",
	            ClientId = "{CLIENT_ID}",
	            Scope = "openid profile"
	        });

	        LoginButton.Clicked += LoginButton_Clicked;
	    }

	    private async void LoginButton_Clicked(object sender, EventArgs e)
	    {
	        var loginResult = await _auth0Client.LoginAsync();

	        var sb = new StringBuilder();

	        if (loginResult.IsError)
	        {
	            ResultLabel.Text = "An error occurred during login...";

	            sb.AppendLine("An error occurred during login:");
	            sb.AppendLine(loginResult.Error);
	        }
	        else
	        {
	            ResultLabel.Text = $"Welcome {loginResult.User.Identity.Name}";

	            sb.AppendLine($"ID Token: {loginResult.IdentityToken}");
	            sb.AppendLine($"Access Token: {loginResult.AccessToken}");
	            sb.AppendLine($"Refresh Token: {loginResult.RefreshToken}");
	            sb.AppendLine();
	            sb.AppendLine("-- Claims --");
	            foreach (var claim in loginResult.User.Claims)
	            {
	                sb.AppendLine($"{claim.Type} = {claim.Value}");
	            }
	        }

	        System.Diagnostics.Debug.WriteLine(sb.ToString());

	    }
    }
}
