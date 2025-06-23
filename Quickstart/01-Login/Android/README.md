# Login
<img src="https://img.shields.io/badge/community-driven-brightgreen.svg"/> <br>

This example shows how to add ***Login/SignUp*** to your Xamarin Android application using the [Auth0 OIDC Client for .NET](https://github.com/auth0/auth0-oidc-client-net).

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/native/xamarin).

This repo is supported and maintained by Community Developers, not Auth0. For more information about different support levels check https://auth0.com/docs/support/matrix.

## Getting started

### Requirements

* Visual Studio for Mac

OR

* Visual Studio 2017
* Xamarin for Visual Studio 4.5

### Installation

1. Create an Auth0 Client and set the callback URL to `com.auth0.quickstart://YOUR_AUTH0_DOMAIN/android/com.auth0.quickstart/callback`. Be sure to replace `YOUR_AUTH0_DOMAIN` with your own Auth0 domain, for example `com.auth0.quickstart://mycompany.auth0.com/android/com.auth0.quickstart/callback`.

2. Set the logout URL to `com.auth0.quickstart://YOUR_AUTH0_DOMAIN/android/com.auth0.quickstart/callback`. Be sure to replace `YOUR_AUTH0_DOMAIN` with your own Auth0 domain, for example `com.auth0.quickstart://mycompany.auth0.com/android/com.auth0.quickstart/callback`.

3. Set the values for the `auth0_domain` and `auth0_client_id` strings inside `/Resources/values/Strings.xml` to the values of your Auth0 Client's **domain** and **Client Id**.

4. Run the application from Visual Studio.

5. Click on the **Login** button in your application in order to Log In with Auth0.

## Usage

### 1. Initialize the OIDC Client

You can initialize Auth0 OIDC Client by creating a new instance of the `Auth0Client` class, passing a `Auth0ClientOptions` instance with the configuration settings. Also set the `Activity` property of the `Auth0ClientOptions` instance to the current activity.

```csharp
// MainActivity.cs

protected override void OnCreate(Bundle bundle)
{
base.OnCreate(bundle);

//...

client = new Auth0Client(new Auth0ClientOptions
{
Domain = Resources.GetString(Resource.String.auth0_domain),
ClientId = Resources.GetString(Resource.String.auth0_client_id)
});
}
```

### 2. Start the Login process

To initiate the login process, you can call the `PrepareLoginAsync` method to obtain the Auth0 authorization URL. You can then create a new browser Intent which will redirect the user to this URL.

```csharp
// MainActivity.cs

private async void LoginButtonOnClick(object sender, EventArgs eventArgs)
{
userDetailsTextView.Text = "";

progress = new ProgressDialog(this);
progress.SetTitle("Log In");
progress.SetMessage("Please wait while redirecting to login screen...");
progress.SetCancelable(false); // disable dismiss by tapping outside of the dialog
progress.Show();

// Prepare for the login
authorizeState = await client.PrepareLoginAsync();

// Send the user off to the authorization endpoint
var uri = Android.Net.Uri.Parse(authorizeState.StartUrl);
var intent = new Intent(Intent.ActionView, uri);
intent.AddFlags(ActivityFlags.NoHistory);
StartActivity(intent);
}
```

### 3. Pass the Callback URL to the OIDC Client

The user will be redirected to the Auth0 Hosted Lock page where they can log in. After the user has authenticated, Auth0 will redirect them back to the **callback URL** which was registered when your created your Client in the Auth0 Dashboard.

You will need to handle this Intent inside your application, and obtain the login information my calling the `ProcessResponseAsync` method.

```csharp
// MainActivity.cs

protected override async void OnNewIntent(Intent intent)
{
base.OnNewIntent(intent);

var loginResult = await client.ProcessResponseAsync(intent.DataString, authorizeState);

//...
}
```

### 4. Process the Login Result

You can check the `LoginResult` returned by calling the `ProcessResponseAsync` to see whether authentication was successful. The `IsError` flag will indicate if there was an error, and if so, you can extract the error message from the `Error` property.

If the authentication was successful, you can obtain the user's information from the claims, and also obtain the tokens.

```csharp
// MainActivity.cs

protected override async void OnNewIntent(Intent intent)
{
base.OnNewIntent(intent);

var loginResult = await client.ProcessResponseAsync(intent.DataString, authorizeState);

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
```

## Contribute

Feel like contributing to this repo? We're glad to hear that! Before you start contributing please visit our [Contributing Guideline](https://github.com/auth0-samples/getting-started/blob/master/CONTRIBUTION.md).

Here you can also find the [PR template](https://github.com/auth0-samples/auth0-xamarin-oidc-samples/blob/master/PULL_REQUEST_TEMPLATE.md) to fill once creating a PR. It will automatically appear once you open a pull request.

## Issues Reporting

Spotted a bug or any other kind of issue? We're just humans and we're always waiting for constructive feedback! Check our section on how to [report issues](https://github.com/auth0-samples/getting-started/blob/master/CONTRIBUTION.md#issues)!

Here you can also find the [Issue template](https://github.com/auth0-samples/auth0-xamarin-oidc-samples/blob/master/ISSUE_TEMPLATE.md) to fill once opening a new issue. It will automatically appear once you create an issue.

## Repo Community

Feel like PRs and issues are not enough? Want to dive into further discussion about the tool? We created topics for each Auth0 Community repo so that you can join discussion on stack available on our repos. Here it is for this one: [auth0-xamarin-oidc-samples](https://community.auth0.com/t/auth0-samples-oss-auth0-xamarin-oidc-samples/15973)

<a href="https://community.auth0.com/">
<img src="/Assets/join_auth0_community_badge.png"/>
</a>

## License

This project is licensed under the MIT license. See the [LICENSE](https://github.com/auth0-samples/auth0-xamarin-oidc-samples/blob/master/LICENSE) file for more info.

## What is Auth0?

Auth0 helps you to:

* Add authentication with [multiple authentication sources](https://docs.auth0.com/identityproviders), either social like
* Google
* Facebook
* Microsoft
* Linkedin
* GitHub
* Twitter
* Box
* Salesforce
* etc.

**or** enterprise identity systems like:
* Windows Azure AD
* Google Apps
* Active Directory
* ADFS
* Any SAML Identity Provider

* Add authentication through more traditional [username/password databases](https://docs.auth0.com/mysql-connection-tutorial)
* Add support for [linking different user accounts](https://docs.auth0.com/link-accounts) with the same user
* Support for generating signed [JSON Web Tokens](https://docs.auth0.com/jwt) to call your APIs and create user identity flow securely
* Analytics of how, when and where users are logging in
* Pull data from other sources and add it to user profile, through [JavaScript rules](https://docs.auth0.com/rules)

## Create a free Auth0 account

* Go to [Auth0 website](https://auth0.com/signup)
* Hit the **SIGN UP** button in the upper-right corner
