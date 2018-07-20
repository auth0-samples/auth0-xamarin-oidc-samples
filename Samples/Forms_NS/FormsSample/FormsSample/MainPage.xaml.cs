using System;
using System.Text;
using Xamarin.Forms;

namespace FormsSample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

		    LoginButton.Clicked += LoginButton_Clicked;
		}

	    private async void LoginButton_Clicked(object sender, EventArgs e)
	    {
	        var authenticationService = DependencyService.Get<IAuthenticationService>();
	        var loginResult = await authenticationService.Authenticate();

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
