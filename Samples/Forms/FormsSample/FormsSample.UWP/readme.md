Install NuGet packages

```text
Install-Package Auth0.OidcClient.UWP
Install-Package CommonServiceLocator
Install-Package Autofac
Install-Package Autofac.Extras.CommonServiceLocator
```

Register services:

```cs
protected override void OnLaunched(LaunchActivatedEventArgs e)
{
    // Some code omitted

    if (rootFrame == null)
    {
        // Create a Frame to act as the navigation context and navigate to the first page
        rootFrame = new Frame();

        rootFrame.NavigationFailed += OnNavigationFailed;

        Xamarin.Forms.Forms.Init(e);

        ContainerBuilder builder = new ContainerBuilder();
        builder.Register(context => new Auth0Client(new Auth0ClientOptions
        {
            Domain = "jerrie.auth0.com",
            ClientId = "vV9twaySQzfGesS9Qs6gOgqDsYDdgoKE"
        })).As<IAuth0Client>();

        IContainer container = builder.Build();

        AutofacServiceLocator asl = new AutofacServiceLocator(container);
        ServiceLocator.SetLocatorProvider(() => asl);

        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
            //TODO: Load state from previously suspended application
        }

        // Place the frame in the current Window
        Window.Current.Content = rootFrame;
    }

    if (rootFrame.Content == null)
    {
        // When the navigation stack isn't restored navigate to the first page,
        // configuring the new page by passing required information as a navigation
        // parameter
        rootFrame.Navigate(typeof(MainPage), e.Arguments);
    }
    // Ensure the current window is active
    Window.Current.Activate();
}
```