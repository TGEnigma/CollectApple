using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectApple.Localization
{
    [ContentProperty(nameof(Text))]
    public class LocalizeExtension : IMarkupExtension
    {
        private PropertyInfo targetProperty;
        private WeakReference<object> targetObjectRef;

        public string Text { get; set; }

        public LocalizeExtension()
        {
            // note: possible memory leak
            Localizer.CultureChanged += CultureChanged;
        }

        private void CultureChanged( object sender, CultureInfo e )
        {
            if ( targetObjectRef.TryGetTarget( out var targetObject ) )
            {
                // target object has not been freed, so apply the changed language text
                targetProperty.SetValue( targetObject, Localizer.Localize( Text ) );
            }
            else
            {
                // target object has been freed, so unsubscribe from the culture change event
                Localizer.CultureChanged -= CultureChanged;
            }
        }

        public object ProvideValue( IServiceProvider serviceProvider )
        {
            try
            {
                // store the target object & target property so we can update the property value dynamically
                // whenever the language is changed
                var provideValueTarget = serviceProvider.GetService<IProvideValueTarget>();
                var bindableProperty = (BindableProperty)provideValueTarget.TargetProperty;
                targetObjectRef = new WeakReference<object>( provideValueTarget.TargetObject );
                targetProperty = provideValueTarget.TargetObject.GetType().GetProperty( bindableProperty.PropertyName );
                return Localizer.Localize( Text );
            }
            catch ( Exception ex )
            {
                return $"<<error:{ex.Message}>>";
            }
        }
    }
}
