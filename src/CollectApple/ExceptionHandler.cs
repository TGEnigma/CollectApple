using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CollectApple
{
    public static class ExceptionHandler
    {
        public static bool HandleException(Exception ex)
        {
            Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            return true;
        }
    }
}
