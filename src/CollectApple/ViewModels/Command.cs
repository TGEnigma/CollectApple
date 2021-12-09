using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    public class Command : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public Command(Action execute, Func<bool> canExecute = null)
        {
            this.execute = (o) => execute();
            this.canExecute = (o) => canExecute();
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return canExecute?.Invoke(parameter) ?? true;
            }
            catch ( AppException ex )
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "OK" );
            }
            catch (Exception ex)
            {
#if !DEBUG
                Shell.Current.DisplayAlert("Error", "Internal error", "OK");
#endif
            }

            return false;
        }

        public void Execute(object parameter)
        {
            if (execute != null)
            {
                try
                {
                    execute(parameter);
                }
                catch ( AppException ex )
                {
                    Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                }
                catch ( Exception ex )
                {
#if !DEBUG
                    Shell.Current.DisplayAlert("Error", "Internal error", "OK");
#endif
                }

            }
        }
    }
}
