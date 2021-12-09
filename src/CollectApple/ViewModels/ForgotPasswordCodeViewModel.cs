using System;
using System.Collections.Generic;
using System.Text;

namespace CollectApple.ViewModels
{
    public class ForgotPasswordCodeViewModel : BaseViewModel
    {
        private string code;

        public string Email { get; }

        public string Code
        {
            get { return code; }
            set { SetProperty(ref code, value); }
        }

        public Command ResetPasswordCommand { get; }

        public ForgotPasswordCodeViewModel(string email)
        {
            Email = email;
            ResetPasswordCommand = new Command(OnResetPassword);
        }

        private async void OnResetPassword(object obj)
        {
            await UserService.ResetPasswordAsync(Email, Code);
        }
    }
}
