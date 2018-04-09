using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth0.OidcClient;
using CommonServiceLocator;
using Xamarin.Forms;

namespace FormsSample
{
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
}
