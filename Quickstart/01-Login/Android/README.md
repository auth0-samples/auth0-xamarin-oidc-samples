# Login

This example shows how to add ***Login/SignUp*** to your Xamarin Android application using the [Auth0 OIDC Client for .NET](https://github.com/auth0/auth0-oidc-client-net).

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/native/xamarin).

## Requirements

* Visual Studio for Mac

OR 

* Visual Studio 2017
* Xamarin for Visual Studio 4.5

## To run this project

1. Create an Auth0 Client and set the callback URL to `com.auth0.quickstart://YOUR_AUTH0_DOMAIN/android/com.auth0.quickstart/callback`. Be sure to replace `YOUR_AUTH0_DOMAIN` with your own Auth0 domain, for example `com.auth0.quickstart://mycompany.auth0.com/android/com.auth0.quickstart/callback`

2. Set the values for the `auth0_domain` and `auth0_client_id` strings inside `/Resources/values/Strings.xml` to the values of your Auth0 Client's **domain** and **Client Id**.

3. Run the application from Visual Studio.

4. Click on the **Login** button in your application in order to Log In with Auth0.

## Important Snippets

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
        ClientId = Resources.GetString(Resource.String.auth0_client_id),
        Activity = this
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

### 4. Pass the Callback URL to the OIDC Client

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

### 3. Process the Login Result

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

