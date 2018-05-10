using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Text.Method;
using Android.Widget;
using Auth0.OidcClient;
using IdentityModel.OidcClient;
using System;
using System.Text;

namespace AndroidSample
{
    [Activity(Label = "AndroidSample", MainLauncher = true, Icon = "@drawable/icon",
        LaunchMode = LaunchMode.SingleTask)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "com.auth0.quickstart",
        DataHost = "@string/auth0_domain",
        DataPathPrefix = "/android/com.auth0.quickstart/callback")]
    public class MainActivity : Activity
    {
        private Auth0Client client;
        private Button loginButton;
        private TextView userDetailsTextView;
        private AuthorizeState authorizeState;
        ProgressDialog progress;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            loginButton = FindViewById<Button>(Resource.Id.LoginButton);
            loginButton.Click += LoginButtonOnClick;

            userDetailsTextView = FindViewById<TextView>(Resource.Id.UserDetailsTextView);
            userDetailsTextView.MovementMethod = new ScrollingMovementMethod();
            userDetailsTextView.Text = String.Empty;

            client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = Resources.GetString(Resource.String.auth0_domain),
                ClientId = Resources.GetString(Resource.String.auth0_client_id)
            });
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (progress != null)
            {
                progress.Dismiss();

                progress.Dispose();
                progress = null;
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            Auth0.OidcClient.ActivityMediator.Instance.Send(intent.DataString);
        }

        private async void LoginButtonOnClick(object sender, EventArgs eventArgs)
        {
            userDetailsTextView.Text = "";

            progress = new ProgressDialog(this);
            progress.SetTitle("Log In");
            progress.SetMessage("Please wait while redirecting to login screen...");
            progress.SetCancelable(false); // disable dismiss by tapping outside of the dialog
            progress.Show();

            // Login
            var loginResult = await client.LoginAsync();

            var sb = new StringBuilder();
            if (loginResult.IsError)
            {
                sb.AppendLine($"An error occurred during login: {loginResult.Error}");
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

            userDetailsTextView.Text = sb.ToString();
        }
    }
}