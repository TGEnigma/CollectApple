using CollectApple.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private string email;

        public string Email
        {
            get { return email; }
            set { SetProperty( ref email, value ); }
        }

        public ImageSource Logo
            => ImageSource.FromResource( "CollectApple.Images.Logo.png" );

        public Command ResetPasswordCommand { get; set; }
        public Command BackToLoginCommand { get; set; }
        public Command EnterCodeCommand { get; set; }

        public ForgotPasswordViewModel()
        {
            ResetPasswordCommand = new Command( OnResetPassword );
            BackToLoginCommand = new Command( OnBackToLogin );
            EnterCodeCommand = new Command( OnEnterCode );
        }

        private async void OnEnterCode()
        {
            try
            {
                await Shell.Current.Navigation.PushAsync( new ForgotPasswordCodePage( Email ) );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        private async void OnResetPassword()
        {
            try
            {
                await UserService.SendResetPasswordEmailAsync( Email );
                await Shell.Current.DisplayAlert( "Notice", "An email has been sent with a link to reset the password.", "OK" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        private async void OnBackToLogin()
        {
            try
            {
                await Shell.Current.GoToAsync( $"//{nameof( LoginPage )}" );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }
    }
}
