using CollectApple.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        private string password;

        public string Email
        {
            get { return email; }
            set { SetProperty( ref email, value ); }
        }

        public string Password
        {
            get { return password; }
            set { SetProperty( ref password, value ); }
        }

        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command ForgotPasswordCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command( OnLoginClicked );
            RegisterCommand = new Command( OnRegisterClicked );
            ForgotPasswordCommand = new Command( OnForgotPasswordClicked );
        }

        private async void OnLoginClicked( object obj )
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync( $"//{nameof( AboutPage )}" );
        }

        private async void OnRegisterClicked( object obj )
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.Navigation.PushAsync( new RegisterPage() );
        }

        private async void OnForgotPasswordClicked( object obj )
        {
            await Shell.Current.Navigation.PushAsync( new ForgotPasswordPage() );
        }
    }
}
