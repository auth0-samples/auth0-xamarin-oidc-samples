﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Text.Method;
using Android.Widget;
using Auth0.OidcClient;
using System;

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
    public class MainActivity : Auth0ClientActivity
    {
        private Auth0Client _auth0Client;
        private Action<string> writeLine;
        private Action clearText;
        private TextView _userDetailsTextView;
        private string accessToken;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _auth0Client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = GetString(Resource.String.auth0_domain),
                ClientId = GetString(Resource.String.auth0_client_id),
                Scope = "openid profile email"
            });

            SetContentView(Resource.Layout.Main);
            FindViewById<Button>(Resource.Id.LoginButton).Click += LoginButtonOnClick;
            FindViewById<Button>(Resource.Id.LogoutButton).Click += LogoutButtonOnClick;
            FindViewById<Button>(Resource.Id.UserButton).Click += UserButtonOnClick;

            _userDetailsTextView = FindViewById<TextView>(Resource.Id.UserDetailsTextView);
            _userDetailsTextView.MovementMethod = new ScrollingMovementMethod();

            writeLine = (s) => _userDetailsTextView.Text += s + "\n";
            clearText = () => _userDetailsTextView.Text = "";
        }

        private async void LoginButtonOnClick(object sender, EventArgs eventArgs)
        {
            clearText();
            writeLine("Starting login...");

            var loginResult = await _auth0Client.LoginAsync();

            if (loginResult.IsError)
            {
                writeLine($"An error occurred during login: {loginResult.Error}");
                return;
            }

            accessToken = loginResult.AccessToken;

            writeLine($"id_token: {loginResult.IdentityToken}");
            writeLine($"access_token: {loginResult.AccessToken}");
            writeLine($"refresh_token: {loginResult.RefreshToken}");

            writeLine($"name: {loginResult.User.FindFirst(c => c.Type == "name")?.Value}");
            writeLine($"email: {loginResult.User.FindFirst(c => c.Type == "email")?.Value}");

            foreach (var claim in loginResult.User.Claims)
            {
                writeLine($"{claim.Type} = {claim.Value}");
            }
        }

        private async void LogoutButtonOnClick(object sender, EventArgs e)
        {
            clearText();
            writeLine("Starting logout...");

            var result = await _auth0Client.LogoutAsync();
            accessToken = null;
            writeLine(result.ToString());
        }

        private async void UserButtonOnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                writeLine("You need to be logged in to get user info");
                return;
            }

            writeLine("Getting user info...");
            var userInfoResult = await _auth0Client.GetUserInfoAsync(accessToken);

            if (userInfoResult.IsError)
            {
                writeLine($"An error occurred getting user info: {userInfoResult.Error}");
                return;
            }

            foreach (var claim in userInfoResult.Claims)
            {
                writeLine($"{claim.Type} = {claim.Value}");
            }
        }
    }
}