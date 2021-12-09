using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CollectApple.Localization
{
    public static class Localizer
    {
        public static event EventHandler<CultureInfo> CultureChanged;

        public static void SetLanguage( string code )
        {
            var cultureInfo = new CultureInfo( code );
            CultureInfo.CurrentUICulture = cultureInfo;
            Strings.Culture = CultureInfo.CurrentUICulture;
            CultureChanged?.Invoke( null, cultureInfo );
        }

        public static string Localize( string key )
        {
            try
            {
                return Strings.ResourceManager.GetString( key, Strings.Culture );
            }
            catch ( Exception ex )
            {
                return $"<<key:{key}>>";
            }
        }
    }
}
