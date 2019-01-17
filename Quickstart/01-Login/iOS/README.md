# Login
<img src="https://img.shields.io/badge/community-driven-brightgreen.svg"/> <br>

This example shows how to add ***Login/SignUp*** to your Xamarin iOS application using the [Auth0 OIDC Client for .NET](https://github.com/auth0/auth0-oidc-client-net).

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/native/xamarin).

This repo is supported and maintained by Community Developers, not Auth0. For more information about different support levels check https://auth0.com/docs/support/matrix .

## Getting started

### Requirements

* Visual Studio for Mac

### Installation

1. Create an Auth0 Client and set the callback URL to `com.auth0.iossample://YOUR_AUTH0_DOMAIN/ios/com.auth0.iossample/callback`. Be sure to replace `YOUR_AUTH0_DOMAIN` with your own Auth0 domain, for example `com.auth0.iossample://mycompany.auth0.com/ios/com.auth0.iossample/callback`

2. Replace the `{DOMAIN}` and `{CLIENT_ID}` placeholders inside the `LoginButton_TouchUpInside` event handler of `MyViewController.cs` with the values of your Auth0 Client's **domain** and **Client Id**.

3. Run the application from Visual Studio.

4. Click on the **Login** button in your application in order to Log In with Auth0.

## Usage

### 1. Initialize the OIDC Client

You can initialize Auth0 OIDC Client by creating a new instance of the `Auth0Client` class, passing a `Auth0ClientOptions` instance with the configuration settings.

```csharp
// MyViewController.cs

private async void LoginButton_TouchUpInside(object sender, EventArgs e)
{
_client = new Auth0Client(new Auth0ClientOptions
{
Domain = "{DOMAIN}",
ClientId = "{CLIENT_ID}",
Controller = this
});

//...
}
```

### 2. Start the Login process

To initiate the login process, you can call the `LoginAsync` method, optionally passing an `extraParameters` parameter containing extra parameters such as the `audience` and `connection`.

```csharp
// MyViewController.cs

private async void LoginButton_TouchUpInside(object sender, EventArgs e)
{
_client = new Auth0Client(new Auth0ClientOptions
{
Domain = "{DOMAIN}",
ClientId = "{CLIENT_ID}",
Controller = this
});

var loginResult = await _client.LoginAsync(null);

//...
}
```

### 3. Process the Login Result

You can check the `LoginResult` returned by calling the `LoginAsync` to see whether authentication was successful. The `IsError` flag will indicate if there was an error, and if so, you can extract the error message from the `Error` property.

If the authentication was successful, you can obtain the user's information from the claims, and also obtain the tokens.

```csharp
// MyViewController.cs

private async void LoginButton_TouchUpInside(object sender, EventArgs e)
{
_client = new Auth0Client(new Auth0ClientOptions
{
Domain = "{DOMAIN}",
ClientId = "{CLIENT_ID}",
Controller = this
});

var loginResult = await _client.LoginAsync(null);

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
```

### 4. Pass the Callback URL to the OIDC Client

After the `LoginAsync` method is called, the OIDC Client will open a Safari browser to display the Auth0 Hosted Lock page. After the user has authenticated, Auth0 will redirect them back to the **callback URL** which was registered when your created your Client in the Auth0 Dashboard.

This will give open your application again, and you will need to pass this URL to the OIDC Client in order for it to complete the authentication process. You can do this by calling `ActivityMediator.Instance.Send()` inside the `OpenUrl` method if your `AppDelegate` class, passing the `url`.

```csharp
// AppDelegate.cs

public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
{
ActivityMediator.Instance.Send(url.AbsoluteString);

return true;
}
```

## Contribute

Feel like contributing to this repo? We're glad to hear that! Before you start contributing please visit our [Contributing Guideline](https://github.com/auth0-community/getting-started/blob/master/CONTRIBUTION.md).

Here you can also find the [PR template](https://github.com/auth0-community/auth0-xamarin-oidc-samples/blob/master/PULL_REQUEST_TEMPLATE.md) to fill once creating a PR. It will automatically appear once you open a pull request.

## Issues Reporting

Spotted a bug or any other kind of issue? We're just humans and we're always waiting for constructive feedback! Check our section on how to [report issues](https://github.com/auth0-community/getting-started/blob/master/CONTRIBUTION.md#issues)!

Here you can also find the [Issue template](https://github.com/auth0-community/auth0-xamarin-oidc-samples/blob/master/ISSUE_TEMPLATE.md) to fill once opening a new issue. It will automatically appear once you create an issue.

## Repo Community

Feel like PRs and issues are not enough? Want to dive into further discussion about the tool? We created topics for each Auth0 Community repo so that you can join discussion on stack available on our repos. Here it is for this one: [auth0-xamarin-oidc-samples](https://community.auth0.com/t/auth0-community-oss-auth0-xamarin-oidc-samples/15973)

<a href="https://community.auth0.com/">
<img src="/Assets/join_auth0_community_badge.png"/>
</a>

## License

This project is licensed under the MIT license. See the [LICENSE](https://github.com/auth0-community/auth0-xamarin-oidc-samples/blob/master/LICENSE) file for more info.

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
