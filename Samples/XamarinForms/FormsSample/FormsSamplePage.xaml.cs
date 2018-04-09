using System;
using Auth0.OidcClient;
using Xamarin.Forms;

namespace FormsSample
{
    public partial class FormsSamplePage : ContentPage
    {
        private readonly IAuth0Client _auth0Client;

        public FormsSamplePage()
        {
            InitializeComponent();

            _auth0Client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "jerrie.auth0.com",
                ClientId = "vV9twaySQzfGesS9Qs6gOgqDsYDdgoKE"
            });

            Login.Clicked += Login_Clicked;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var result = await _auth0Client.LoginAsync();

            System.Diagnostics.Debug.WriteLine(result.User.Identity.Name);
        }
    }
}
