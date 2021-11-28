using CollectApple.Services;
using CollectApple.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectApple.Views
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class CollectionsPage : ContentPage
    {
        public CollectionsPage()
        {
            InitializeComponent();
            BindingContext = new CollectionsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ( (CollectionsViewModel)BindingContext ).OnAppearing();
        }
    }
}