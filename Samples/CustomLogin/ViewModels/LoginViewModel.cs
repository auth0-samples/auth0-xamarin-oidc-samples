using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomLogin
{
    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Command LoginCommand { get; set; }
        public Command SignUpCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            SignUpCommand = new Command(async () => await ExecuteSignUpCommand());
        }

        private async Task ExecuteLoginCommand()
        {
        }

        private async Task ExecuteSignUpCommand()
        {
        }
    }
}
