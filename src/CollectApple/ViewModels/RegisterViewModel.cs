using CollectApple.Views;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string email;
        private string password;
        private string passwordCheck;

        public string Email
        {
            get {  return email; }
            set {  SetProperty ( ref email, value ); }
        }

        public string Password
        {
            get { return password; }
            set { SetProperty( ref password, value ); }
        }

        public string PasswordCheck
        {
            get { return passwordCheck; }
            set { SetProperty( ref passwordCheck, value ); }
        }

        public Command RegisterCommand { get; }

        public Command LoginCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command( OnRegisterClicked );
            LoginCommand = new Command( OnLoginClicked );
        }

        private async void OnRegisterClicked( object obj )
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync( $"//{nameof( AboutPage )}" );
        }

        private async void OnLoginClicked( object obj )
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync( $"//{nameof( LoginPage )}" );
        }
    }
}
