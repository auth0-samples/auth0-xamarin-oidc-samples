using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CustomLogin
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            Navigation.PushModalAsync(new LoginPage());
        }
    }
}
