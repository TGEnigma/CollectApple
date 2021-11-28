using CollectApple.ViewModels;
using CollectApple.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CollectApple
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for sub pages
            Routing.RegisterRoute( nameof( ItemDetailPage ), typeof( ItemDetailPage ) );
            Routing.RegisterRoute( nameof( NewItemPage ), typeof( NewItemPage ) );
            Routing.RegisterRoute( nameof( CollectionDetailPage ), typeof( CollectionDetailPage ) );
            Routing.RegisterRoute( nameof( CollectibleDetailPage ), typeof( CollectibleDetailPage ) );

            CurrentItem = loginPage;
        }

        private async void OnMenuItemClicked( object sender, EventArgs e )
        {
            await Shell.Current.GoToAsync( "//LoginPage" );
        }
    }
}
