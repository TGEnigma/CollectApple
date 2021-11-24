using CollectApple.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command( OnLoginClicked );
        }

        private async void OnLoginClicked( object obj )
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync( $"//{nameof( AboutPage )}" );
        }
    }

    public class RegisterViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command( OnRegisterClicked );
        }

        private async void OnRegisterClicked( object obj )
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync( $"//{nameof( AboutPage )}" );
        }
    }
}
