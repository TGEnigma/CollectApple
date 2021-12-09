using CollectApple.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CollectApple.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private LanguageViewModel language;

        public LanguageViewModel Language
        {
            get => language;
            set => SetProperty(ref language, value);  
        }

        public IReadOnlyCollection<LanguageViewModel> Languages { get; } = new LanguageViewModel[]
        {
            new LanguageViewModel(Strings.English, "en-US"),
            new LanguageViewModel(Strings.Dutch, "nl")
        };

        public ICommand SaveCommand { get; }

        public SettingsViewModel()
        {
            Title = Strings.Settings;
            SaveCommand = new Command( OnSave );
        }

        private async void OnSave()
        {
            try
            {
                Localizer.SetLanguage( Language.Code );
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }
    }
}
