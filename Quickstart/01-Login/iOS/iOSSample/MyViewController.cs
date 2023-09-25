using System.Text;
using static Foundation.NSBundle;
using Auth0.OidcClient;

namespace iOSSample;

public partial class MyViewController : UIViewController {
    private Auth0Client _client;

    public MyViewController () : base (nameof (MyViewController), null)
	{
	}

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        // Perform any additional setup after loading the view, typically from a nib.

        UserDetailsTextView.Text = String.Empty;

        LoginButton.TouchUpInside += LoginButton_TouchUpInside;

    }

    public override void DidReceiveMemoryWarning ()
	{
		base.DidReceiveMemoryWarning ();
		// Release any cached data, images, etc that aren't in use.
	}

    private async void LoginButton_TouchUpInside(object sender, EventArgs e)
    {

        var callbackUrl = $"{MainBundle.BundleIdentifier}://frdrkprck.eu.auth0.com/ios/{MainBundle.BundleIdentifier}/callback";

        _client = new Auth0Client(new Auth0ClientOptions
        {
            Domain = "{DOMAIN}",
            ClientId = "{CLIENT_ID}",
            Scope = "openid profile",
            RedirectUri = callbackUrl,
            PostLogoutRedirectUri = callbackUrl
        });

        var loginResult = await _client.LoginAsync();

        var sb = new StringBuilder();

        if (loginResult.IsError)
        {
            sb.AppendLine("An error occurred during login:");
            sb.AppendLine(loginResult.Error);
        }
        else
        {
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

        UserDetailsTextView.Text = sb.ToString();
    }
}

