using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CustomLogin
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new LoginViewModel(Navigation);
        }
    }
}
