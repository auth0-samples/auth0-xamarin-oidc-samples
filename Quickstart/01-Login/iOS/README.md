# Login

This example shows how to add ***Login/SignUp*** to your Xamarin iOS application using the [Auth0 OIDC Client for .NET](https://github.com/auth0/auth0-oidc-client-net).

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/native/xamarin).

## Requirements

* Visual Studio for Mac

## To run this project

1. Create an Auth0 Client and set the callback URL to `com.auth0.iossample://YOUR_AUTH0_DOMAIN/ios/com.auth0.iossample/callback`. Be sure to replace `YOUR_AUTH0_DOMAIN` with your own Auth0 domain, for example `com.auth0.iossample://mycompany.auth0.com/ios/com.auth0.iossample/callback`

2. Replace the `{DOMAIN}` and `{CLIENT_ID}` placeholders inside the `LoginButton_TouchUpInside` event handler of `MyViewController.cs` with the values of your Auth0 Client's **domain** and **Client Id**.

3. Run the application from Visual Studio.

4. Click on the **Login** button in your application in order to Log In with Auth0.

## Important Snippets

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