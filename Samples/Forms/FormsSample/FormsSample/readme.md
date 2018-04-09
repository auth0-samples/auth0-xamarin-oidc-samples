Install NuGet packages

```text
Install-Package Auth0.OidcClient.Core
Install-Package CommonServiceLocator
```

Create login button

```xml
<StackLayout Margin="40">
    <Button Text="Login" x:Name="Login" />
</StackLayout>
```

Create event handler:

```cs
public partial class MainPage : ContentPage
{
	private readonly IAuth0Client _auth0Client;

	public MainPage()
	{
		InitializeComponent();

		_auth0Client = ServiceLocator.Current.GetInstance<IAuth0Client>();

		Login.Clicked += Login_Clicked;
	}

	private async void Login_Clicked(object sender, EventArgs e)
	{
		var result = await _auth0Client.LoginAsync();
	}
}
```