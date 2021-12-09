using CollectApple.Services;
using CollectApple.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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

        public ImageSource Logo
            => ImageSource.FromResource( "CollectApple.Images.Logo.png" );

        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command ForgotPasswordCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command( OnLoginClicked );
            RegisterCommand = new Command( OnRegisterClicked );
            ForgotPasswordCommand = new Command( OnForgotPasswordClicked );
        }

        private async void OnLoginClicked()
        {
            try
            {
                if ( string.IsNullOrWhiteSpace( Email ) )
                    throw new AppException( "Please fill in your email." );

                if ( string.IsNullOrWhiteSpace( Password ) )
                    throw new AppException( "Please fill in your password." );

                UserService.SignIn( Email, Password );

                await Shell.Current.GoToAsync( $"//{nameof( CollectionsPage )}" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        private async void OnRegisterClicked()
        {
            try
            {
                await Shell.Current.Navigation.PushAsync( new RegisterPage() );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        private async void OnForgotPasswordClicked()
        {
            try
            {
                await Shell.Current.Navigation.PushAsync( new ForgotPasswordPage() );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }
    }
}
